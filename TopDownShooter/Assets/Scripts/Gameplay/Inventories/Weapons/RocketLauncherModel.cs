using Gameplay.Characters;
using UnityEngine;
using Utilities;

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
            DebugDraw.DrawSphere(position, AreaOfEffect, Color.magenta, 0.3f);
            foreach (var collider in colliders)
            {
                if (collider.TryGetComponent(out IDamageable damageable))
                {
                    if (!damageable.IsAlive) continue;
                    damageable.OnDamageReceived(Damage, ArmorPiercingDamage);
                }
            }
        }
    }
}