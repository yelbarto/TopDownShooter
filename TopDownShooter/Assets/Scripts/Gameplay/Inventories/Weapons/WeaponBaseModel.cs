using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
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
        public int BurstFireAmmoAmount { get; }
        
        public Dictionary<Type, bool> UpgradableItems { get; } = new();
        
        protected readonly int ArmorPiercingDamage;
        private readonly WeaponBaseView _weaponBaseView;
        private DateTime _lastFireTime;

        public WeaponBaseModel(WeaponData weaponData, Transform parent)
        {
            RateOfFire = weaponData.RateOfFire;
            Damage = weaponData.Damage;
            Range = weaponData.Range;
            AreaOfEffect = weaponData.AreaOfEffect;
            ArmorPiercingPercentage = weaponData.ArmorPiercingPercentage;
            BurstFireAmmoAmount = weaponData.BurstFireAmmoAmount;
            _weaponBaseView = Object.Instantiate(weaponData.WeaponPrefab, parent);
            _weaponBaseView.transform.localPosition = Vector3.zero;
            ArmorPiercingDamage = (int) Math.Ceiling(Damage * (ArmorPiercingPercentage / 100f));
        }

        private bool CanUpgradeWeapon(Type itemType)
        {
            return !UpgradableItems.ContainsKey(itemType) || !UpgradableItems[itemType];
        }

        public void RemoveUpgrades()
        {
            UpgradableItems.Clear();
        }
        
        public void UpgradeWeapon(IUpgradableItem itemBase)
        {
            var type = itemBase.GetType();
            if (!CanUpgradeWeapon(type))
            {
                Debug.Log($"Can't upgrade item {itemBase.GetType().Name} to weapon {GetType().Name}");
                return;
            }

            Debug.Log($"Upgrade weapon {GetType().Name} with item {itemBase.GetType().Name}");
            UpgradableItems[type] = true;
            itemBase.UseUpgrade(this);
        }
        
        public void SwitchWeapon(bool toThis)
        {
            _weaponBaseView.gameObject.SetActive(toThis);
        }

        public void Fire(IDamageable owner)
        {
            if (DateTime.Now.Subtract(_lastFireTime).TotalSeconds < 1f / RateOfFire)
                return;
            _lastFireTime = DateTime.Now;
            _weaponBaseView.PlayFireAnimation_Async(owner, Range, OnImpact, BurstFireAmmoAmount).Forget();
        }
        
        protected virtual void OnImpact(IDamageable enemy, Vector3 position)
        {
            Debug.Log($"On Impact: Damage {Damage}, ArmorPiercingDamage {ArmorPiercingDamage}");
            enemy.OnDamageReceived(Damage, ArmorPiercingDamage);
        }
    }
}