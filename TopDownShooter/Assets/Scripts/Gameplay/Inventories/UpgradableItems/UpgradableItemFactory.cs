using Gameplay.Helpers;
using UnityEngine;

namespace Gameplay.Inventories.UpgradableItems
{
    public class UpgradableItemFactory
    {
        private readonly Transform[] _upgradableItemSpawnPoints;
        private readonly Transform _itemParent;

        public UpgradableItemFactory(Transform[] upgradableItemSpawnPoints, Transform itemParent)
        {
            _upgradableItemSpawnPoints = upgradableItemSpawnPoints;
            _itemParent = itemParent;
        }
        
        public void CreateObject(int index)
        {
            var upgradableItemData = (index % 3) switch
            {
                0 => GameplaySettingsProvider.Instance.BarrelData,
                1 => GameplaySettingsProvider.Instance.ScopeData,
                _ => GameplaySettingsProvider.Instance.ArmorPiercingRoundsData
            };
            var upgradableItem = Object.Instantiate(upgradableItemData.UpgradableItem, 
                _upgradableItemSpawnPoints[index].position, Quaternion.identity, _itemParent);
            upgradableItem.SetUp(Random.Range(upgradableItemData.UpgradeValue.x, 
                upgradableItemData.UpgradeValue.y));
        }
    }
}