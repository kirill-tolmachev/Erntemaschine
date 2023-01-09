using System.Collections.Generic;
using Assets.Scripts.Messages;
using Erntemaschine.Enemies;
using Erntemaschine.Messages.Impl;
using UnityEngine;
using Zenject;

namespace Erntemaschine.Controllers
{
    internal class EnemyController : MonoBehaviour
    {
        [SerializeField]
        private Enemy[] _initEnemies;

        [Inject]
        private IMessageBus _messageBus;

        private readonly HashSet<Enemy> _spawnedEnemies = new HashSet<Enemy>();

        public static EnemyController Instance { get; private set; }

        void Awake()
        {
            foreach (var enemy in _initEnemies)
            {
                _spawnedEnemies.Add(enemy);
            }
            
            _messageBus.Subscribe<ObjectDied>(OnObjectDied);
            Instance = this;
        }

        private void OnObjectDied(ObjectDied obj)
        {
            if (!obj.Object.TryGetComponent(out Enemy enemy))
                return;

            _messageBus.Publish(new ExplosionOccured(enemy.transform.position));
            Destroy(enemy);
        }

        public IReadOnlyCollection<Enemy> Enemies => _spawnedEnemies;

        public void Destroy(Enemy enemy)
        {
            _spawnedEnemies.Remove(enemy);
            Destroy(enemy.gameObject);
        }

        public void AddEnemy(Enemy enemy) => _spawnedEnemies.Add(enemy);
    }
}
