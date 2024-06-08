using UnityEngine;
using UnityEngine.Serialization;

namespace Gameplay.Inventories.Weapons
{
    [CreateAssetMenu(fileName = "WeaponData", menuName = "TopDownShooter/WeaponData")]
    public class WeaponData : ScriptableObject
    {
        [SerializeField] private int _rateOfFire;
        [SerializeField] private int _damage;
        [SerializeField] private int _range;
        [SerializeField] private int _areaOfEffect;
        [SerializeField] private int _armorPiercingPercentage;
        [SerializeField] private int _burstFireAmmoAmount;
        [SerializeField] private WeaponBaseView _weaponPrefab;

        public int RateOfFire => _rateOfFire;
        public int Damage => _damage;
        public int Range => _range;
        public int AreaOfEffect => _areaOfEffect;
        public int ArmorPiercingPercentage => _armorPiercingPercentage;
        public int BurstFireAmmoAmount => _burstFireAmmoAmount;
        public WeaponBaseView WeaponPrefab => _weaponPrefab;
    }
}