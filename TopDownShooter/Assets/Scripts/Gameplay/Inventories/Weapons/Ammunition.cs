using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Gameplay.Characters;
using UnityEngine;
using IPoolable = Utilities.IPoolable;

namespace Gameplay.Inventories.Weapons
{
    public class Ammunition : MonoBehaviour, IPoolable
    {
        [SerializeField] private float _speed = 10;
        
        private Action<IDamageable> _impactAction;
        private CancellationTokenSource _moveCts;
        private Transform _transform;
        private IDamageable _owner;

        private void Awake()
        {
            _transform = transform;
        }

        public void OnDespawn()
        {
            _impactAction = null;
            _owner = null;
        }

        public void OnSpawn()
        {
        }

        public void Fire(Action<IDamageable> impactAction, Vector3 destination, 
            IDamageable owner, Vector3 ignitionPosition)
        {
            _transform.position = ignitionPosition;
            _transform.LookAt(destination);
            _impactAction = impactAction;
            _owner = owner;
            
            Move_Async(destination).Forget();
        }
        
        private async UniTask Move_Async(Vector3 destination)
        {
            _moveCts = new CancellationTokenSource();
            await _transform.DOMove(destination, _speed).SetSpeedBased()
                .ToUniTask(TweenCancelBehaviour.KillAndCancelAwait, _moveCts.Token);
            AmmunitionProvider.Instance.ReturnAmmunition(this);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamageable damageable))
            {
                if (damageable == _owner)
                    return;
                _moveCts?.Cancel();
                _impactAction?.Invoke(damageable);
                AmmunitionProvider.Instance.ReturnAmmunition(this);
            }
        }
    }
}