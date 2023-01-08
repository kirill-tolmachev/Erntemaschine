using System.Collections.Generic;
using Assets.Scripts.Messages;
using Erntemaschine.Messages.Impl;
using Erntemaschine.Vehicles;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;

namespace Erntemaschine.Controllers
{
    internal class ShootController : MonoBehaviour
    {
        [Inject] private IMessageBus _messageBus;

        [SerializeField] private Bullet _bullet;

        [Inject] private DiContainer _container;

        private HashSet<Bullet> _bullets = new HashSet<Bullet>();

        private ObjectPool<Bullet> _bulletPool;

        [SerializeField] 
        private float _destroyBulletsAt = 100f;

        private void Awake()
        {
            _messageBus.Subscribe<GunShot>(Shoot);
            _messageBus.Subscribe<BulletHit>(OnBulletHit);
            _bulletPool = new ObjectPool<Bullet>(InstantiateBullet, actionOnGet: x => x.gameObject.SetActive(true), actionOnRelease: x => x.gameObject.SetActive(false));
        }

        private Bullet InstantiateBullet()
        {
            return _container.InstantiatePrefabForComponent<Bullet>(_bullet);
        }

        private void Shoot(GunShot shot)
        {
            var angle = Mathf.Atan2(shot.Direction.y, shot.Direction.x) - Mathf.PI / 2;
            var rotation = new Vector3(0f, 0f, angle * Mathf.Rad2Deg);

            var instance = _bulletPool.Get();
            instance.transform.SetPositionAndRotation(shot.Origin, Quaternion.Euler(rotation));//_container.InstantiatePrefabForComponent<Transform>(_bullet, shot.Origin, Quaternion.Euler(rotation), transform);
            instance.transform.SetParent(transform, true);
            instance.Speed = shot.BulletSpeed;
            instance.Damage = shot.BulletDamage;

            _bullets.Add(instance);
        }

        private void Update()
        {
            var itemsToRemove = new List<Bullet>();
            foreach (var bullet in _bullets)
            {
                bullet.transform.position += bullet.Speed * Time.deltaTime * bullet.transform.up;
                if (bullet.transform.position.sqrMagnitude > _destroyBulletsAt * _destroyBulletsAt)
                    itemsToRemove.Add(bullet);
            }

            foreach (var item in itemsToRemove)
            {
                _bullets.Remove(item);
                _bulletPool.Release(item);
            }
        }

        private void OnBulletHit(BulletHit obj)
        {
            var item = obj.Bullet;
            _bullets.Remove(item);
            _bulletPool.Release(item);

        }
    }
}
