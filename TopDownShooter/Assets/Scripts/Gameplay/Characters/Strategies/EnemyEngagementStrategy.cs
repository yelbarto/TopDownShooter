using Cysharp.Threading.Tasks;
using UnityEngine;
using Utilities;

namespace Gameplay.Characters.Strategies
{
    public class EnemyEngagementStrategy : EnemyStrategyBase
    {
        [SerializeField] private ColliderListener _colliderListener;
        [SerializeField] private CharacterMovementComponent _characterMovementComponent;
        [SerializeField] private CharacterViewBase _characterViewBase;
        
        private Transform _engageTarget;

        private void Awake()
        {
            _colliderListener.OnColliderEnter += OnTriggerEnter;
            _colliderListener.OnColliderExit += OnTriggerExit;
        }

        protected override void StartStrategy()
        {
            
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (StrategyCts == null) return;
            if (StrategyCts.IsCancellationRequested) return;
            if (!other.TryGetComponent(out IDamageable damageable) ||
                damageable.CharacterType != CharacterType.Player) return;
            _characterMovementComponent.LookAt(other.transform.position);
            _engageTarget = other.transform;
            EngageTarget_Async().Forget();
        }

        private async UniTask EngageTarget_Async()
        {
            while (StrategyCts.IsCancellationRequested && _engageTarget != null)
            {
                _characterMovementComponent.LookAt(_engageTarget.position);
                _characterViewBase.OnFire?.Invoke();
                await UniTask.Delay(100, cancellationToken: StrategyCts.Token);
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (!StrategyCts.IsCancellationRequested && other.TryGetComponent(out IDamageable damageable) &&
                damageable.CharacterType == CharacterType.Player)
            {
                _engageTarget = null;
            }
        }

        private void OnDestroy()
        {
            _colliderListener.OnColliderEnter -= OnTriggerEnter;
            _colliderListener.OnColliderExit -= OnTriggerExit;
        }
    }
}