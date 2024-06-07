using UnityEngine;
using Utilities;

namespace Gameplay.Inventories.Weapons
{
    public class AmmunitionProvider : MonoSingleton<AmmunitionProvider>
    {
        [SerializeField] private Ammunition _ammunitionPrefab;
        [SerializeField] private Transform _poolParent;
        [SerializeField] private int _ammunitionWarmUpAmount = 10;
        
        private GameObjectPool<Ammunition> _pool;

        protected override void SingletonStarted()
        {
            base.SingletonStarted();
            _pool = new GameObjectPool<Ammunition>(_ammunitionPrefab, _poolParent);
            _pool.LoadPrefab(_ammunitionWarmUpAmount);
        }

        public Ammunition SpawnAmmunition()
        {
            return _pool.Get();
        }

        public void ReturnAmmunition(Ammunition ammunition)
        {
            _pool.Return(ammunition);
        }
    }
}