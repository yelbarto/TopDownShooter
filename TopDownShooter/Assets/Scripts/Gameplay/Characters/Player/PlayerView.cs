using System;
using Gameplay.Inventories.UpgradableItems;
using UnityEngine;

namespace Gameplay.Characters.Player
{
    public class PlayerView : CharacterViewBase
    {
        [SerializeField] private float _speed;
        
        public Action<IUpgradableItem> TryUpgradeWeapon;
        
        private Transform _transform;

        protected override void OnAwake()
        {
            InputController.Instance.OnMousePositionChanged += OnMousePositionChanged;
            InputController.Instance.OnMovementKeyPressed += OnMovementKeyPressed;
            _transform = transform;
        }
        
        private void OnMousePositionChanged(Vector3 mousePosition)
        {
            var targetPosition = new Vector3(mousePosition.x, transform.position.y, mousePosition.z);
            _transform.LookAt(targetPosition);
        }
        
        private void OnMovementKeyPressed(Vector2 movement)
        {
            var movementVector = new Vector3(movement.x, 0, movement.y);
            _transform.Translate(movementVector * (Time.deltaTime * _speed));
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
            InputController.Instance.OnMousePositionChanged -= OnMousePositionChanged;
            InputController.Instance.OnMovementKeyPressed -= OnMovementKeyPressed;
        }
    }
}