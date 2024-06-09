using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Gameplay.Characters;
using UnityEngine;
using Utilities;
using IPoolable = Utilities.IPoolable;

namespace Gameplay.Inventories.Weapons
{
    public class Ammunition : MonoBehaviour, IPoolable
    {
        [SerializeField] private float _speed = 10;
        [SerializeField] private ColliderListener _colliderListener;
        
        private Action<IDamageable, Vector3> _impactAction;
        private CancellationTokenSource _moveCts;
        private Transform _transform;
        private IDamageable _owner;

        private void Awake()
        {
            _transform = transform;
            _colliderListener.OnColliderEnter += OnTriggerEnter;
        }

        public void OnDespawn()
        {
            _impactAction = null;
            _owner = null;
        }

        public void OnSpawn()
        {
            gameObject.SetActive(true);
        }

        public void Fire(Action<IDamageable, Vector3> impactAction, Vector3 destination, 
            IDamageable owner, Vector3 ignitionPosition)
        {
            _transform.position = ignitionPosition;
            _transform.LookAt(destination);
            _impactAction = impactAction;
            _owner = owner;
            Debug.DrawLine(ignitionPosition, destination, Color.red, 0.2f);
            
            Move_Async(destination).Forget();
        }
        
        private async UniTask Move_Async(Vector3 destination)
        {
            _moveCts = new CancellationTokenSource();
            await _transform.DOMove(destination, _speed).SetSpeedBased().SetEase(Ease.Linear)
                .ToUniTask(TweenCancelBehaviour.KillAndCancelAwait, _moveCts.Token);
            AmmunitionProvider.Instance.ReturnAmmunition(this);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out IDamageable damageable)) return;
            if (damageable.IsAlive == false) return;
            if (damageable == _owner) return;
            _moveCts?.Cancel();
            _impactAction?.Invoke(damageable, transform.position);
            AmmunitionProvider.Instance.ReturnAmmunition(this);
        }

        private void OnDestroy()
        {
            _colliderListener.OnColliderEnter -= OnTriggerEnter;
        }
    }
}