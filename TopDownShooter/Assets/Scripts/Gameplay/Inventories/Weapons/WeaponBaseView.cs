using System;
using Gameplay.Characters;
using Gameplay.Inventories.Models;
using UnityEngine;

namespace Gameplay.Inventories.Weapons
{
    public class WeaponBaseView : InventoryBaseView
    {
        [SerializeField] private Transform _firePoint;
        
        public void PlayFireAnimation(IDamageable owner, float range, Action<IDamageable> onImpact)
        {
            var ammo = AmmunitionProvider.Instance.SpawnAmmunition();
            var ignitionPosition = _firePoint.position;
            var destination = range * _firePoint.forward + ignitionPosition;
            ammo.Fire(onImpact, destination, owner, ignitionPosition);
        }
    }
}