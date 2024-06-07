using System;
using System.Collections.Generic;
using Gameplay.Characters;
using Gameplay.Inventories.Models;
using Gameplay.Inventories.UpgradableItems;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Gameplay.Inventories.Weapons
{
    public class WeaponBaseModel : InventoryBaseModel, IWeapon
    {
        public int RateOfFire { get; }
        public int Damage { get; set; }
        public int Range { get; set; }
        public int ArmorPiercingPercentage { get; set; }
        public int AreaOfEffect { get; }
        
        public Dictionary<Type, bool> UpgradableItems { get; } = new();
        
        private readonly WeaponBaseView _weaponBaseView;
        private readonly int _armorPiercingDamage;
        private DateTime _lastFireTime;

        public WeaponBaseModel(WeaponData weaponData)
        {
            RateOfFire = weaponData.RateOfFire;
            Damage = weaponData.Damage;
            Range = weaponData.Range;
            AreaOfEffect = weaponData.AreaOfEffect;
            ArmorPiercingPercentage = weaponData.ArmorPiercingPercentage;
            _weaponBaseView = Object.Instantiate(weaponData.WeaponPrefab);
            _armorPiercingDamage = Damage * (ArmorPiercingPercentage / 100);
        }

        private bool CanUpgradeWeapon(Type itemType)
        {
            return !UpgradableItems.ContainsKey(itemType) || !UpgradableItems[itemType];
        }
        
        public void UpgradeWeapon(IUpgradableItem itemBase)
        {
            var type = itemBase.GetType();
            if (!CanUpgradeWeapon(type)) return;

            UpgradableItems[type] = true;
            itemBase.UseUpgrade(this);
            Debug.Log("Upgrade weapon");
        }

        public void Fire(IDamageable owner)
        {
            if (_lastFireTime.AddSeconds(1.0 / RateOfFire) < DateTime.Now)
                return;
            Debug.Log("Fire");
            _lastFireTime = DateTime.Now;
            _weaponBaseView.PlayFireAnimation(owner, Range, OnImpact);
        }
        
        protected virtual void OnImpact(IDamageable enemy)
        {
            enemy.OnDamageReceived(Damage, _armorPiercingDamage);
        }
    }
}