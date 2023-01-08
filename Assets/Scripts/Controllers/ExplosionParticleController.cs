using Assets.Scripts.Messages;
using Erntemaschine.Messages.Impl;
using UnityEngine;
using Zenject;

namespace Erntemaschine.Controllers
{
    internal class ExplosionParticleController : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem _particleSystem;

        [SerializeField]
        private ParticleSystem _hitParticleSystem;

        [Inject]
        private IMessageBus _messageBus;

        [Inject] 
        private DiContainer _container;

        private void Start()
        {
            _messageBus.Subscribe<ExplosionOccured>(x => MakeExplosion(x.Position));
            _messageBus.Subscribe<BulletHit>(x => MakeHit(x.Position));
            _messageBus.Subscribe<EnemyAttacked>(x => MakeHit(x.Part.transform.position));
        }

        private void MakeExplosion(Vector3 position)
        {
            _container.InstantiatePrefab(_particleSystem, position, Quaternion.identity, transform);
        }

        private void MakeHit(Vector3 position)
        {
            _container.InstantiatePrefab(_hitParticleSystem, position, Quaternion.identity, transform);
        }
    }
}
