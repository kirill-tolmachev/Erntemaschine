using System;
using System.Linq;
using Assets.Scripts.Messages;
using Cysharp.Threading.Tasks;
using Erntemaschine.Controllers;
using Erntemaschine.Enemies;
using Erntemaschine.Messages.Impl;
using UnityEngine;
using Zenject;

namespace Erntemaschine.Vehicles.Processors
{
    internal class GunProcessor : Processor
    {
        private Func<Processor> _rotate;

        private Func<Processor> _shoot;

        [SerializeField]
        private float _maxRange = 100f;

        [SerializeField]
        private float _rotationSpeed;

        [SerializeField] 
        private float _shootingInterval;

        [SerializeField] 
        private Transform _gunPoint;

        [SerializeField]
        private Transform _gunOrigin;

        [SerializeField] 
        private float _bulletSpeed;

        [SerializeField]
        private float _bulletDamage;

        [Inject(Optional = true)]
        private EnemyController _enemyController;

        [Inject]
        private IMessageBus _messageBus;

        [SerializeField] 
        private LayerMask _layerMask;

        private float _lastShootTime;

        private void Start()
        {
            _rotate = UseSlot("rotate");
            _shoot = UseSlot("attack");
        }

        private void Update()
        {
            if (_rotate.TryRead(out var rotate))
            {
                Rotate(rotate);
            }

            if (_shoot.TryRead(out var shoot))
            {
                Shoot(shoot);
            }
        }

        private Vector3 Direction => _gunPoint.position - _gunOrigin.position;

        private void Rotate(float value)
        {
            var enemies = _enemyController.Enemies.OrderBy(x => Mathf.Abs(AngularDistance(_gunPoint.position, Direction, x.transform.position)));
            var closest = enemies.FirstOrDefault();

            if (closest == null)
            {
                return;
            }

            if (AimIntersects(closest))
            {
                return;
            }

            var diff = AngularDistance(_gunPoint.position, Direction, closest.transform.position);
            // _rotationSpeed * Time.deltaTime;

            var old = _gunOrigin.transform.rotation.eulerAngles;
            //TODO: Проверить если при добавлении вращения уедем сильнее чем надо. Уехать либо на вращение, либо на точный поворот до противника.
            _gunOrigin.transform.rotation = Quaternion.Euler(0f, 0f, old.z - Mathf.Sign(diff) * _rotationSpeed * value * Time.deltaTime);
        }

        private float AngularDistance(Vector3 origin, Vector3 direction, Vector3 position)
        {
            var normed = position - origin;
            return Mathf.Atan2(direction.y - normed.y, direction.x - normed.x);
        }

        private void Shoot(float value)
        {
            var now = Time.time;
            if (now - _lastShootTime < _shootingInterval / value)
            {
                return;
            }

            var enemies = _enemyController.Enemies;
            bool isInRange = enemies.Any(AimIntersects);
            if (!isInRange)
            {
                return;
            }

            _messageBus.Publish(new GunShot(_gunPoint.position, Direction, _bulletSpeed, _bulletDamage)).Forget();
            _lastShootTime = now; 
        }

        bool AimIntersects(Enemy enemy)
        {
            if (!enemy.IsSeen)
                return false;

            Debug.DrawRay(_gunOrigin.position, Direction * _maxRange, Color.yellow);
            var hit = Physics2D.Raycast(_gunOrigin.position, Direction * _maxRange, Mathf.Infinity);

            return hit.transform == enemy.transform;
        }
    }
}
