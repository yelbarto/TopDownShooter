using Cysharp.Threading.Tasks;
using Gameplay.Helpers;
using Gameplay.Inventories.UpgradableItems;
using Gameplay.Inventories.Weapons;
using UnityEngine;

namespace Gameplay.Characters.Player
{
    public class PlayerModel : CharacterModelBase
    {
        private PlayerView _playerView;
        
        public PlayerModel(Vector3 originPoint, Transform parent, WeaponFactory weaponFactory) : 
            base(GameplaySettingsProvider.Instance.PlayerData, originPoint, parent, "Player", weaponFactory)
        {
            InputController.Instance.OnAlphaKeyPressed += OnAlphaKeyPressed;
        }

        protected sealed override void SetUpViewEvents()
        {
            base.SetUpViewEvents();
            _playerView = (PlayerView) CharacterView;
            _playerView.TryUpgradeWeapon += UseUpgrade;
            CreateWeapons();
        }

        protected override UniTask WaitForRespawn()
        {
            Weapons[WeaponIndex].RemoveUpgrades();
            return UniTask.CompletedTask;
        }

        private void UseUpgrade(IUpgradableItem upgradableItem)
        {
            Weapons[WeaponIndex].UpgradeWeapon(upgradableItem);
        }

        private void CreateWeapons()
        {
            Weapons = new WeaponBaseModel[3];
            Weapons[0] = CreateWeapon(WeaponType.Pistol);
            Weapons[1] = CreateWeapon(WeaponType.Rifle);
            Weapons[2] = CreateWeapon(WeaponType.RocketLauncher);
            Weapons[0].SwitchWeapon(true);
        }

        private void OnAlphaKeyPressed(int key)
        {
            SwitchWeapon(key - 1);
        }

        private void SwitchWeapon(int index)
        {
            Weapons[WeaponIndex].SwitchWeapon(false);
            WeaponIndex = index;
            Weapons[WeaponIndex].SwitchWeapon(true);
        }

        public override void Dispose()
        {
            base.Dispose();
            if (InputController.Instance == null) return;
            InputController.Instance.OnAlphaKeyPressed -= OnAlphaKeyPressed;
            _playerView.TryUpgradeWeapon -= UseUpgrade;
        }
    }
}