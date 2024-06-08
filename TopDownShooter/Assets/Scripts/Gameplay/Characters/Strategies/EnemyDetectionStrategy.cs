using Cysharp.Threading.Tasks;
using UnityEngine;
using Utilities;

namespace Gameplay.Characters.Strategies
{
    public class EnemyDetectionStrategy : EnemyStrategyBase
    {
        [SerializeField] private ColliderListener _colliderListener;
        [SerializeField] private CharacterMovementComponent _characterMovementComponent;
        
        private Transform _chasingTarget;
        private Transform _transform;

        private void Awake()
        {
            _colliderListener.OnColliderEnter += OnTriggerEnter;
            _colliderListener.OnColliderExit += OnTriggerExit;
            _transform = transform;
        }

        public override void Deactivate()
        {
            base.Deactivate();
            _chasingTarget = null;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (StrategyCts == null) return;
            if (StrategyCts.IsCancellationRequested) return;
            if (_chasingTarget != null) return;
            if (!other.TryGetComponent(out IDamageable damageable) ||
                damageable.CharacterType != CharacterType.Player) return;
            _chasingTarget = other.transform;
            Chase_Async().Forget();
        }

        private async UniTask Chase_Async()
        {
            while (_chasingTarget != null && !StrategyCts.IsCancellationRequested)
            {
                var chasingPosition = _chasingTarget.position - _transform.position;
                _characterMovementComponent.LookAt(chasingPosition);
                //Normalize the vector to make the enemy move with the same speed in all directions
                _characterMovementComponent.Move(new Vector2(chasingPosition.x, chasingPosition.z).normalized);
                await UniTask.NextFrame(StrategyCts.Token);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (!StrategyCts.IsCancellationRequested && other.transform == _chasingTarget)
            {
                _chasingTarget = null;
            }
        }

        protected override void StartStrategy()
        {
            //Do nothing
        }

        private void OnDestroy()
        {
            _colliderListener.OnColliderEnter -= OnTriggerEnter;
            _colliderListener.OnColliderExit -= OnTriggerExit;
        }
    }
}