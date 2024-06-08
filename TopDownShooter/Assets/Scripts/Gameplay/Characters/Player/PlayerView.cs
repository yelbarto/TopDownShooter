using System;
using Gameplay.Inventories.UpgradableItems;
using UnityEngine;

namespace Gameplay.Characters.Player
{
    public class PlayerView : CharacterViewBase
    {
        [SerializeField] private Transform _cameraPositioner;
        
        public Action<IUpgradableItem> TryUpgradeWeapon;
        
        protected override void OnAwake()
        {
            CharacterType = CharacterType.Player;
            InputController.Instance.OnMousePositionChanged += OnMousePositionChanged;
            InputController.Instance.OnMovementKeyPressed += OnMovementKeyPressed;
            InputController.Instance.OnMouseClicked += OnMouseClicked;
            var mainCamera = Camera.main;
            if (mainCamera == null)
                throw new Exception("Main camera is not found");
            var cameraTransform = mainCamera.transform;
            cameraTransform.SetParent(_cameraPositioner);
            cameraTransform.localPosition = Vector3.zero;
        }

        private void OnMouseClicked()
        {
            OnFire?.Invoke();
        }
        
        private void OnMousePositionChanged(Vector3 mousePosition)
        {
            CharacterMovementComponent.LookAt(mousePosition);
        }
        
        private void OnMovementKeyPressed(Vector2 movement)
        {
            CharacterMovementComponent.Move(movement);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IUpgradableItem upgradableItem))
            {
                TryUpgradeWeapon?.Invoke(upgradableItem);
            }
        }

        private void OnDestroy()
        {
            if (InputController.Instance == null) return;
            InputController.Instance.OnMousePositionChanged -= OnMousePositionChanged;
            InputController.Instance.OnMovementKeyPressed -= OnMovementKeyPressed;
        }
    }
}