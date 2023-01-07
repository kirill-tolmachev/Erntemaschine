using System.Collections.Generic;
using Erntemaschine.Enemies;
using UnityEngine;

namespace Erntemaschine.Controllers
{
    internal class EnemyController : MonoBehaviour
    {
        [SerializeField]
        private Enemy[] _initEnemies;

        private readonly HashSet<Enemy> _spawnedEnemies = new HashSet<Enemy>();

        void Awake()
        {
            foreach (var enemy in _initEnemies)
            {
                _spawnedEnemies.Add(enemy);
            }
            
        }

        public IReadOnlyCollection<Enemy> Enemies => _spawnedEnemies;

        public void Destroy(Enemy enemy)
        {
            _spawnedEnemies.Remove(enemy);
            Destroy(enemy.gameObject);
        }
    }
}
