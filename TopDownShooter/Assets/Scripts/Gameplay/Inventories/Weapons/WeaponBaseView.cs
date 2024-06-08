using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Gameplay.Characters;
using Gameplay.Inventories.Models;
using UnityEngine;

namespace Gameplay.Inventories.Weapons
{
    public class WeaponBaseView : InventoryBaseView
    {
        [SerializeField] private Transform _firePoint;
        [SerializeField] private float _ammoDelay;
        private CancellationTokenSource _lifetimeCts;

        private void Awake()
        {
            _lifetimeCts = new CancellationTokenSource();
        }

        public async UniTask PlayFireAnimation_Async(IDamageable owner, float range, 
            Action<IDamageable, Vector3> onImpact, int burstFireAmmo)
        {
            for (var i = 0; i < burstFireAmmo; i++)
            {
                var ammo = AmmunitionProvider.Instance.SpawnAmmunition();
                var ignitionPosition = _firePoint.position;
                var destination = range * _firePoint.forward + ignitionPosition;
                ammo.Fire(onImpact, destination, owner, ignitionPosition);
                if (i < burstFireAmmo - 1)
                    await UniTask.Delay(TimeSpan.FromSeconds(_ammoDelay), cancellationToken: _lifetimeCts.Token);
            }
        }

        private void OnDestroy()
        {
            _lifetimeCts?.Cancel();
        }
    }
}