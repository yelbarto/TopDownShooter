using System.Threading;
using UnityEngine;

namespace Gameplay.Characters.Strategies
{
    public abstract class EnemyStrategyBase : MonoBehaviour
    {
        protected CancellationTokenSource StrategyCts;
        
        public virtual void Deactivate()
        {
            StrategyCts?.Cancel();
        }

        public void Activate()
        {
            StrategyCts = new CancellationTokenSource();
            StartStrategy();
        }
        
        protected abstract void StartStrategy();
    }
}