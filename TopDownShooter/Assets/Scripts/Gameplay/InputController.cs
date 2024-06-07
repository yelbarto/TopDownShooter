using System;
using UnityEngine;
using Utilities;

namespace Gameplay
{
    public class InputController : MonoSingleton<InputController>
    {
        [SerializeField] private float _distanceFromCamera = 10f;
        [SerializeField] private Camera _mainCamera;
        
        public Action<Vector3> OnMousePositionChanged;
        public Action<int> OnAlphaKeyPressed;
        public Action<Vector2> OnMovementKeyPressed;
        public Action OnMouseClicked;
        private Vector2 _forwardVector;
        private Vector2 _backwardVector;
        private Vector2 _leftVector;
        private Vector2 _rightVector;

        protected override void SingletonAwakened()
        {
            base.SingletonAwakened();
            _forwardVector = new Vector2(0, 1);
            _backwardVector = new Vector2(0, -1);
            _leftVector = new Vector2(-1, 0);
            _rightVector = new Vector2(1, 0);
        }

        private void Update()
        {
            CheckWeaponSwitch();
            CheckMovement();
            CheckRotation();
            CheckFireInput();
        }

        private void CheckFireInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                OnMouseClicked?.Invoke();
            }
        }

        private void CheckRotation()
        {
            var mouseScreenPosition = Input.mousePosition;
            mouseScreenPosition.z = _distanceFromCamera; // Set the distance from the camera
            var mouseWorldPosition = _mainCamera.ScreenToWorldPoint(mouseScreenPosition);
            Debug.Log("Mouse World Position: " + mouseWorldPosition);
            OnMousePositionChanged?.Invoke(mouseWorldPosition);
        }

        private void CheckWeaponSwitch()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                OnAlphaKeyPressed?.Invoke(1);
            } else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                OnAlphaKeyPressed?.Invoke(2);
            } else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                OnAlphaKeyPressed?.Invoke(3);
            }
        }

        private void CheckMovement()
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                OnMovementKeyPressed?.Invoke(_forwardVector);
            } 
            if (Input.GetKeyDown(KeyCode.A))
            {
                OnMovementKeyPressed?.Invoke(_leftVector);
            } 
            if (Input.GetKeyDown(KeyCode.S))
            {
                OnMovementKeyPressed?.Invoke(_backwardVector);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                OnMovementKeyPressed?.Invoke(_rightVector);
            }
        }
    }
}