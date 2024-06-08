using System;
using System.Collections.Generic;
using Gameplay.Characters;
using Gameplay.Characters.Enemy;
using Gameplay.Characters.Player;
using Gameplay.Helpers;
using Gameplay.Inventories.UpgradableItems;
using Gameplay.Inventories.Weapons;
using UnityEngine;

namespace Gameplay
{
    public class GameplayManager : MonoBehaviour
    {
        [Header("General")]
        [SerializeField] private Transform _spawnableParent;
        
        [Header("Player")]
        [SerializeField] private Transform _playerSpawnPoint;
        
        [Header("Enemy")]
        [SerializeField] private Transform[] _enemySpawnPoints;
        
        [Header("Upgradable Item")]
        [SerializeField] private Transform[] _upgradableItemSpawnPoints;

        private readonly List<IDisposable> _disposables = new();
        private UpgradableItemFactory _upgradableItemFactory;
        private WeaponFactory _weaponFactory;

        private void Awake()
        {
            _weaponFactory = new WeaponFactory();
            GeneratePlayer();
            GenerateEnemies();
            GenerateUpgradableItems();
        }

        private void GeneratePlayer()
        {
            var playerModel = new PlayerModel(_playerSpawnPoint.position, _spawnableParent, _weaponFactory);
            _disposables.Add(playerModel);
        }

        private void GenerateEnemies()
        {
            for (var i = 0; i < _enemySpawnPoints.Length; i++)
            {
                var enemyModel = new EnemyModel(GameplaySettingsProvider.Instance.EnemyDataArray[i % 4], 
                    _enemySpawnPoints[i].position, _spawnableParent, $"Enemy{i}", _weaponFactory);
                _disposables.Add(enemyModel);
            }
        }

        private void GenerateUpgradableItems()
        {
            _upgradableItemFactory = new UpgradableItemFactory(_upgradableItemSpawnPoints, _spawnableParent);
            for (var i = 0; i < _upgradableItemSpawnPoints.Length; i++)
            {
                _upgradableItemFactory.CreateObject(i);
            }
        }
        
        private void OnDestroy()
        {
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }
        }
    }
}