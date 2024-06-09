using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Utilities
{
    [RequireComponent(typeof(ParticleSystem))]
    public class PoolableParticleSystem : MonoBehaviour, IPoolable
    {
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private float _particleLifetime = 1f;
        
        private CancellationTokenSource _lifetimeCts;

        private void Awake()
        {
            _lifetimeCts = new CancellationTokenSource();
        }

        public void Play()
        {
            _particleSystem.Play();
        }

        public void Stop(bool withChildren = true, 
            ParticleSystemStopBehavior stopBehavior = ParticleSystemStopBehavior.StopEmittingAndClear)
        {
            _particleSystem.Stop(withChildren, stopBehavior);
        }

        public async UniTask WaitParticleAnimation_Async()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_particleLifetime), cancellationToken: _lifetimeCts.Token);
        }

        public void OnDespawn()
        {
            
        }

        public void OnSpawn()
        {
            
        }

        private void OnDestroy()
        {
            _lifetimeCts?.Cancel();
        }
    }
}