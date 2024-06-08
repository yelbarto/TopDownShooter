using System;
using Gameplay.Helpers;
using UnityEngine;

namespace Gameplay.Inventories.Weapons
{
    public class WeaponFactory
    {
        public WeaponBaseModel CreateObject(WeaponType weaponType, Transform weaponHolder)
        {
            var weapon = weaponType switch
            {
                WeaponType.RocketLauncher => new RocketLauncherModel(
                    GameplaySettingsProvider.Instance.RocketLauncherData, weaponHolder),
                WeaponType.Pistol => new WeaponBaseModel(GameplaySettingsProvider.Instance.PistolData, weaponHolder),
                WeaponType.Rifle => new WeaponBaseModel(GameplaySettingsProvider.Instance.RifleData, weaponHolder),
                _ => throw new ArgumentOutOfRangeException(nameof(weaponType), weaponType, null)
            };

            return weapon;
        }
    }
}