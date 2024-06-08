using Gameplay.Characters;
using UnityEngine;

namespace Gameplay.Inventories.Weapons
{
    public class RocketLauncherModel : WeaponBaseModel
    {
        public RocketLauncherModel(WeaponData weaponData, Transform parent) : base(weaponData, parent)
        {
        }

        protected override void OnImpact(IDamageable enemy, Vector3 position)
        {
            Debug.Log($"On Impact: Damage {Damage}, ArmorPiercingDamage {ArmorPiercingDamage}");
            var colliders = Physics.OverlapSphere(position, AreaOfEffect);
            foreach (var collider in colliders)
            {
                if (collider.TryGetComponent(out IDamageable damageable))
                    damageable.OnDamageReceived(Damage, ArmorPiercingDamage);
            }
        }
    }
}