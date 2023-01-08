using System.Linq;
using Assets.Scripts.Messages;
using DG.Tweening;
using Erntemaschine.Controllers;
using Erntemaschine.Messages.Impl;
using Erntemaschine.Vehicles;
using UnityEngine;
using Zenject;

namespace Erntemaschine.Enemies
{
    internal class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private float _speed;

        [SerializeField] private Enemy _enemy;

        [SerializeField] private CharacterController2D _characterController;

        private Part _target;

        [Inject] private IMessageBus _messageBus;

        [Inject] private MapController _mapController;

        private bool _isAttacking;

        [SerializeField]
        private float _attackInterval;

        private float _lastAttackTime;

        private void Start()
        {
            _messageBus.Subscribe<ObjectDied>(OnObjectDied);
        }

        private void OnObjectDied(ObjectDied obj)
        {
            if (!obj.Object.TryGetComponent(out Part part))
                return;

            if (part != _target)
                return;

            _target = null;
        }

        private void Update()
        {
            _isAttacking = false;
            if (_target == null)
            {
                if (_mapController.SpawnedParts.Count == 0)
                    return;

                _target = PickTarget();
            }

            if (_target == null)
                return;

            if (Vector2.Distance(transform.position, _target.transform.position) > 1f)
            {
                transform.position = Vector2.MoveTowards(transform.position, _target.transform.position, _speed * Time.deltaTime);
                //_characterController.move(movement);
            }
            else
            {
                _isAttacking = true;
            }

            var now = Time.time;
            if (_isAttacking && (now - _lastAttackTime) > _attackInterval)
            {
                DOTween.Shake(() => transform.position, v => transform.position = v.WithZ(transform.position.z), 1f,
                    0.04f, 10);
                _messageBus.Publish(new EnemyAttacked(_enemy, _target));
                _lastAttackTime = now;
            }
            
        }

        private Part PickTarget()
        {
            return _mapController.SpawnedParts.OrderByDescending(x => x.TargetPriority / (Vector3.Distance(transform.position, x.transform.position) + 0.5f)).FirstOrDefault();
        }


    }
}
