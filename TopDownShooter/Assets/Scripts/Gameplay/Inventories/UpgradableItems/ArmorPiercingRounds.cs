using Gameplay.Inventories.Weapons;

namespace Gameplay.Inventories.UpgradableItems
{
    public class ArmorPiercingRounds : UpgradableItemBase
    {
        protected override void AddToWeapon(IWeapon weapon)
        {
            weapon.ArmorPiercingPercentage += UpgradeValue;
            if (weapon.ArmorPiercingPercentage > 100)
            {
                weapon.ArmorPiercingPercentage = 100;
            }
        }
    }
}