using UnityEngine;

namespace Gameplay.Inventories.UpgradableItems
{
    [CreateAssetMenu(fileName = "UpgradableItemData", menuName = "TopDownShooter/UpgradableItemData")]
    public class UpgradableItemData : ScriptableObject
    {
        [SerializeField] private UpgradableItemBase _upgradableItem;
        [SerializeField] private Vector2Int _upgradeValue;
        
        public UpgradableItemBase UpgradableItem => _upgradableItem;
        public Vector2Int UpgradeValue => _upgradeValue;
    }
}