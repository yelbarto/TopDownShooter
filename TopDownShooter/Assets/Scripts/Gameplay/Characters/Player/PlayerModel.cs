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
        
        public PlayerModel(Vector3 originPoint, Transform parent) : 
            base(GameplaySettingsProvider.Instance.PlayerData, originPoint, parent)
        {
            InputController.Instance.OnAlphaKeyPressed += OnAlphaKeyPressed;
            InputController.Instance.OnMouseClicked += OnMouseClicked;
            CreateWeapons();
            
            SetUpViewEvents();
        }

        protected sealed override void SetUpViewEvents()
        {
            base.SetUpViewEvents();
            _playerView = (PlayerView) CharacterView;
            _playerView.TryUpgradeWeapon += UseUpgrade;
        }

        protected override UniTask WaitForRespawn()
        {
            return UniTask.CompletedTask;
        }

        private void UseUpgrade(IUpgradableItem upgradableItem)
        {
            Weapons[WeaponIndex].UpgradeWeapon(upgradableItem);
        }

        private void CreateWeapons()
        {
            Weapons = new WeaponBaseModel[3];
            Weapons[0] = new WeaponBaseModel(GameplaySettingsProvider.Instance.PistolData, CharacterView.WeaponHolder);
            Weapons[1] = new WeaponBaseModel(GameplaySettingsProvider.Instance.RifleData, CharacterView.WeaponHolder);
            Weapons[2] = new RocketLauncherModel(GameplaySettingsProvider.Instance.RocketLauncherData, 
                CharacterView.WeaponHolder);
            Weapons[0].SwitchWeapon(true);
        }
        
        private void OnMouseClicked()
        {
            Weapons[WeaponIndex].Fire(CharacterView);
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
            InputController.Instance.OnMouseClicked -= OnMouseClicked;
            _playerView.TryUpgradeWeapon -= UseUpgrade;
        }
    }
}