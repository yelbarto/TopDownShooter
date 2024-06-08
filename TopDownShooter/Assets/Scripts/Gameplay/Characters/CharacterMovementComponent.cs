using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Gameplay.Characters
{
    public class CharacterMovementComponent : MonoBehaviour
    {
        [SerializeField] private Transform _rotationTransform;
        [SerializeField] private float _planeBorders = 49.5f;
        [SerializeField] private float _speed = 10;
        
        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }
        
        public void LookAt(Vector3 target)
        {
            var targetPosition = new Vector3(target.x, _rotationTransform.position.y, target.z);
            _rotationTransform.LookAt(targetPosition);
        }

        public async UniTask Move_Async(Vector3 target, CancellationToken token)
        {
            var position = new Vector3(target.x, 0, target.y);
            position.x = Mathf.Clamp(position.x, -_planeBorders, _planeBorders);
            position.z = Mathf.Clamp(position.z, -_planeBorders, _planeBorders);
            await _transform.DOMove(target, _speed).SetSpeedBased().SetEase(Ease.Linear)
                .ToUniTask(TweenCancelBehaviour.KillAndCancelAwait, token);
        }

        public void Move(Vector2 movement)
        {
            var movementVector = new Vector3(movement.x, 0, movement.y);
            _transform.Translate(movementVector * (Time.deltaTime * _speed));
            var position = _transform.position;
            position.x = Mathf.Clamp(position.x, -_planeBorders, _planeBorders);
            position.z = Mathf.Clamp(position.z, -_planeBorders, _planeBorders);
            _transform.position = position;
        }
    }
}