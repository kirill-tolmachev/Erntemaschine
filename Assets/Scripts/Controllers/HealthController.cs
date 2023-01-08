using Assets.Scripts.Messages;
using Erntemaschine.Messages.Impl;
using Erntemaschine.Vehicles;
using UnityEngine;
using Zenject;

namespace Erntemaschine.Controllers
{
    internal class HealthController : MonoBehaviour
    {
        [Inject]
        private IMessageBus _messageBus;

        private void Start()
        {
            _messageBus.Subscribe<BulletHit>(OnBulletHit);
            _messageBus.Subscribe<EnemyAttacked>(OnEnemyAttacked);
        }

        private void OnEnemyAttacked(EnemyAttacked obj)
        {
            if (!obj.Part.TryGetComponent(out Health health))
                return;

            health.Value -= obj.Enemy.Damage;
        }

        private void OnBulletHit(BulletHit obj)
        {
            if (!obj.Target.TryGetComponent(out Health health))
                return;

            health.Value -= obj.Bullet.Damage;
        }
    }
}
