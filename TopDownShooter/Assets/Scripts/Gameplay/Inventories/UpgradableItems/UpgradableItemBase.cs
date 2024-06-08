using Gameplay.Inventories.Models;
using Gameplay.Inventories.Weapons;

namespace Gameplay.Inventories.UpgradableItems
{
    public abstract class UpgradableItemBase : InventoryBaseView, IUpgradableItem
    {
        protected int UpgradeValue;

        public void SetUp(int upgradeValue)
        {
            UpgradeValue = upgradeValue;
        }
        
        public virtual void UseUpgrade(IWeapon weaponBaseModel)
        {
            Destroy(gameObject);
        }
        
        protected abstract void AddToWeapon(IWeapon weapon);
    }
}