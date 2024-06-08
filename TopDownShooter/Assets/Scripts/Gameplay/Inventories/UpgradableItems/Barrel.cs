using Gameplay.Inventories.Weapons;

namespace Gameplay.Inventories.UpgradableItems
{
    public class Barrel : UpgradableItemBase
    {
        protected override void AddToWeapon(IWeapon weapon)
        {
            weapon.Damage += UpgradeValue;
        }
    }
}