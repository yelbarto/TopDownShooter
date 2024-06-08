using Gameplay.Inventories.Weapons;

namespace Gameplay.Inventories.UpgradableItems
{
    public class Scope : UpgradableItemBase
    {
        protected override void AddToWeapon(IWeapon weapon)
        {
            weapon.Range += UpgradeValue;
        }
    }
}