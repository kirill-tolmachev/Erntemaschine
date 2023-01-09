using Erntemaschine.Enemies;
using UnityEngine;
using Zenject;

namespace Erntemaschine.Controllers
{
    internal class EnemySpawner : MonoBehaviour
    {
        [SerializeField]
        private float _spawnInterval;

        [SerializeField] private float _enemyCoord;

        [SerializeField]
        private MapController _mapController;

        [SerializeField] 
        private Enemy _enemyPrefab;

        [SerializeField]
        private EnemyController _enemyController;

        [Inject]
        private DiContainer _container;

        private float _lastSpawnTime;

        private void Update()
        {
            var now = Time.time;
            if (now - _lastSpawnTime > _spawnInterval)
            {
                SpawnWave();
                _lastSpawnTime = now;
            }
        }

        private void SpawnWave()
        {
            int count = _mapController.SpawnedParts.Count;
            if (count < 7)
                return;

            int enemyCount = count / 3;
            for (int i = 0; i < enemyCount; i++)
            {
                SpawnEnemy(); 
            }
        }

        private void SpawnEnemy()
        {
            var position = new Vector2(_enemyCoord * Mathf.Sin(Random.value * Mathf.PI * 2), _enemyCoord * Mathf.Cos(Random.value * Mathf.PI * 2));
            var enemy = _container.InstantiatePrefabForComponent<Enemy>(_enemyPrefab, position, Quaternion.identity, _enemyController.transform);

            _enemyController.AddEnemy(enemy);
        }

    }
}
