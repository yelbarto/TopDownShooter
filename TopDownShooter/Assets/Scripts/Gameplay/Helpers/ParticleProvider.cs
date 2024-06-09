using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Utilities;

namespace Gameplay.Helpers
{
    public class ParticleProvider : MonoSingleton<ParticleProvider>
    {
        [SerializeField] private List<ParticleDataStructure> _poolableParticleList;
        [SerializeField] private Transform _particleParent;
        
        private Dictionary<ParticleSystemType, GameObjectPool<PoolableParticleSystem>> _poolDictionary = new();

        private void Start()
        {
            foreach (var particleDataStructure in _poolableParticleList)
            {
                var pool = new GameObjectPool<PoolableParticleSystem>(particleDataStructure.ParticleSystem, 
                    _particleParent);
                pool.LoadPrefab(particleDataStructure.WarmUpAmount);
                _poolDictionary.Add(particleDataStructure.ParticleSystemType, pool);
            }
        }

        public async UniTask PlayAndReturnParticle_Async(ParticleSystemType particleSystemType, Vector3 position)
        {
            var poolableParticleSystem = _poolDictionary[particleSystemType].Get(_particleParent);
            poolableParticleSystem.transform.position = position;
            poolableParticleSystem.Play();
            await poolableParticleSystem.WaitParticleAnimation_Async();
            _poolDictionary[particleSystemType].Return(poolableParticleSystem);
        }
        
        
        public void GetParticle(ParticleSystemType particleSystemType)
        {
            _poolDictionary[particleSystemType].Get(_particleParent);
        }
        
        public void ReturnParticle(ParticleSystemType particleSystemType, PoolableParticleSystem poolableParticleSystem)
        {
            _poolDictionary[particleSystemType].Return(poolableParticleSystem);
        }
    }

    [Serializable]
    internal struct ParticleDataStructure
    {
        [SerializeField] private PoolableParticleSystem _particleSystem;
        [SerializeField] private ParticleSystemType _particleSystemType;
        [SerializeField] private int _warmUpAmount;
        
        public ParticleSystemType ParticleSystemType => _particleSystemType;
        public PoolableParticleSystem ParticleSystem => _particleSystem;
        public int WarmUpAmount => _warmUpAmount;
    }

    public enum ParticleSystemType
    {
        RocketLauncher
    }
}