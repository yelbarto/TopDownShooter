using Gameplay.Characters;
using Gameplay.Inventories.UpgradableItems;
using Gameplay.Inventories.Weapons;
using UnityEngine;
using Utilities;

namespace Gameplay.Helpers
{ 
    public class GameplaySettingsProvider : MonoSingleton<GameplaySettingsProvider>
    {
        [SerializeField] private CharacterData _playerData;
        [SerializeField] private CharacterData _enemyData;
        [SerializeField] private WeaponData _rifleData;
        [SerializeField] private WeaponData _pistolData;
        [SerializeField] private WeaponData _rocketLauncherData;
        [SerializeField] private UpgradableItemData _scopeData;
        [SerializeField] private UpgradableItemData _barrelData;
        [SerializeField] private UpgradableItemData _armorPiercingRoundsData;
        
        public CharacterData PlayerData => _playerData;
        public CharacterData EnemyData => _enemyData;
        public WeaponData RifleData => _rifleData;
        public WeaponData PistolData => _pistolData;
        public WeaponData RocketLauncherData => _rocketLauncherData;
        public UpgradableItemData ScopeData => _scopeData;
        public UpgradableItemData BarrelData => _barrelData;
        public UpgradableItemData ArmorPiercingRoundsData => _armorPiercingRoundsData;
    }
}