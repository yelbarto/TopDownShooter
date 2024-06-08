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
                var isCancelled = await _characterMovementComponent
                    .Move_Async(target, StrategyCts.Token).SuppressCancellationThrow();
                if (isCancelled && StrategyCts.IsCancellationRequested)
                    break;
                // Look around
                await UniTask.Delay(Random.Range(1000, 3000), cancellationToken: StrategyCts.Token);
            }
        }
    }
}