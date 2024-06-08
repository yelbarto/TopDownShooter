using Gameplay.Inventories.Weapons;

namespace Gameplay.Inventories.UpgradableItems
{
    public interface IUpgradableItem
    {
        void UseUpgrade(IWeapon weaponBaseModel);
    }
}