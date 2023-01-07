using System.Collections.Generic;
using Assets.Scripts.Messages;
using Erntemaschine.Messages.Impl;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;

namespace Erntemaschine.Controllers
{
    internal class ShootController : MonoBehaviour
    {
        [Inject] private IMessageBus _messageBus;

        [SerializeField] private Transform _bullet;

        [Inject] private DiContainer _container;

        private HashSet<Transform> _bullets = new HashSet<Transform>();

        private ObjectPool<Transform> _bulletPool;

        [SerializeField]
        private float _bulletSpeed;

        [SerializeField] 
        private float _destroyBulletsAt = 100f;

        private void Awake()
        {
            _messageBus.Subscribe<GunShot>(Shoot);
            _bulletPool = new ObjectPool<Transform>(InstantiateBullet);
        }

        private Transform InstantiateBullet()
        {
            return _container.InstantiatePrefabForComponent<Transform>(_bullet);
        }

        private void Shoot(GunShot shot)
        {
            var angle = Mathf.Atan2(shot.Direction.y, shot.Direction.x) - Mathf.PI / 2;
            var rotation = new Vector3(0f, 0f, angle * Mathf.Rad2Deg);

            var instance = _bulletPool.Get();
            instance.SetPositionAndRotation(shot.Origin, Quaternion.Euler(rotation));//_container.InstantiatePrefabForComponent<Transform>(_bullet, shot.Origin, Quaternion.Euler(rotation), transform);
            instance.SetParent(transform, true);
            _bullets.Add(instance);
        }

        private void Update()
        {
            var itemsToRemove = new List<Transform>();
            foreach (var bullet in _bullets)
            {
                bullet.transform.position += bullet.transform.up * _bulletSpeed * Time.deltaTime;
                if (bullet.transform.position.sqrMagnitude > _destroyBulletsAt * _destroyBulletsAt)
                    itemsToRemove.Add(bullet);
            }

            foreach (var item in itemsToRemove)
            {
                _bullets.Remove(item);
                _bulletPool.Release(item);
            }
        }

    }
}
