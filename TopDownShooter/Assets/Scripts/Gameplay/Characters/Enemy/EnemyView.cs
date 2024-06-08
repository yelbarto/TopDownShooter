using Gameplay.Characters.Strategies;
using UnityEngine;

namespace Gameplay.Characters.Enemy
{
    public class EnemyView : CharacterViewBase
    {
        [SerializeField] private EnemyStrategyBase[] _enemyStrategyArray;

        protected override void OnAwake()
        {
            base.OnAwake();
            foreach (var enemyStrategy in _enemyStrategyArray)
                enemyStrategy.Activate();
            CharacterType = CharacterType.Enemy;
        }

        public override void Kill()
        {
            foreach (var enemyStrategy in _enemyStrategyArray)
                enemyStrategy.Deactivate();
            base.Kill();
        }

        public override void Reinitialize(Vector3 position)
        {
            foreach (var enemyStrategy in _enemyStrategyArray)
                enemyStrategy.Activate();
            base.Reinitialize(position);
        }
    }
}