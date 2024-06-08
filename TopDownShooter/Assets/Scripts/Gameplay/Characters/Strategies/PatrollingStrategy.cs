using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Gameplay.Characters.Strategies
{
    public class PatrollingStrategy : EnemyStrategyBase
    {
        [SerializeField] private CharacterMovementComponent _characterMovementComponent;
        
        protected override void StartStrategy()
        {
            Patrol_Async().Forget();
        }

        private async UniTask Patrol_Async()
        {
            while (!StrategyCts.IsCancellationRequested)
            {
                var target = new Vector3(Random.Range(-49.5f, 49.5f), 0, Random.Range(-49.5f, 49.5f));
                _characterMovementComponent.LookAt(target);
                await _characterMovementComponent.Move_Async(target, StrategyCts.Token);
                // Look around
                await UniTask.Delay(Random.Range(1000, 3000), cancellationToken: StrategyCts.Token);
            }
        }
    }
}