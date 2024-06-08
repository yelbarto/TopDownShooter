using System;
using System.Linq;
using UnityEngine;
using Utilities;

namespace Gameplay
{
    public class InputController : MonoSingleton<InputController>
    {
        [SerializeField] private float _maxRaycastDistance = 50f;
        [SerializeField] private Camera _mainCamera;
        
        public Action<Vector3> OnMousePositionChanged;
        public Action<int> OnAlphaKeyPressed;
        public Action<Vector2> OnMovementKeyPressed;
        public Action OnMouseClicked;
        private Vector2 _forwardVector;
        private Vector2 _backwardVector;
        private Vector2 _leftVector;
        private Vector2 _rightVector;
        private int _layerMask;

        private void Start()
        {
            _forwardVector = new Vector2(0, 1);
            _backwardVector = new Vector2(0, -1);
            _leftVector = new Vector2(-1, 0);
            _rightVector = new Vector2(1, 0);
            _layerMask = ~LayerMask.GetMask("Detection");
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
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, _maxRaycastDistance, _layerMask))
            {
                var hitPosition = hit.point;
                OnMousePositionChanged?.Invoke(hitPosition);
            }
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
            if (Input.GetKey(KeyCode.W))
            {
                OnMovementKeyPressed?.Invoke(_forwardVector);
            } 
            if (Input.GetKey(KeyCode.A))
            {
                OnMovementKeyPressed?.Invoke(_leftVector);
            } 
            if (Input.GetKey(KeyCode.S))
            {
                OnMovementKeyPressed?.Invoke(_backwardVector);
            }
            if (Input.GetKey(KeyCode.D))
            {
                OnMovementKeyPressed?.Invoke(_rightVector);
            }
        }
    }
}