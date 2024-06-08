using Gameplay.Characters.Strategies;
using UnityEngine;

namespace Gameplay.Characters.Enemy
{
    public class EnemyView : CharacterViewBase
    {
        [SerializeField] private EnemyStrategyBase _enemyStrategy;

        protected override void OnAwake()
        {
            base.OnAwake();
            _enemyStrategy.Activate();
            CharacterType = CharacterType.Enemy;
        }

        public override void Kill()
        {
            _enemyStrategy.Deactivate();
            base.Kill();
        }

        public override void Reinitialize(Vector3 position)
        {
            _enemyStrategy.Activate();
            base.Reinitialize(position);
        }
    }
}