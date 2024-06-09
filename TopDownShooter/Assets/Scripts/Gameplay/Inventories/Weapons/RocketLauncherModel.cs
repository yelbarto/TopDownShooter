using Cysharp.Threading.Tasks;
using Gameplay.Characters;
using Gameplay.Helpers;
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
            ParticleProvider.Instance.PlayAndReturnParticle_Async(ParticleSystemType.RocketLauncher, position).Forget();
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