using Assets.Scripts.Messages;
using Cysharp.Threading.Tasks;
using Erntemaschine.Messages.Impl;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Erntemaschine.Vehicles
{
    internal class Bullet : MonoBehaviour
    {
        [SerializeField] 
        private LayerMask _targetMask;

        public float Damage;

        public float Speed;

        public Transform Author;

        [Inject]
        private IMessageBus _messageBus;

        private void OnCollisionEnter2D(Collision2D col)
        {
            var other = col.collider;
            if (!_targetMask.HasLayer(other.gameObject.layer))
                return;

            if (other.transform == Author)
                return;

            if (!other.gameObject.TryGetComponent(out Health _)) 
                return;

            var contact = col.GetContact(0);
            _messageBus.Publish(new BulletHit(this, other.gameObject, contact.point)).Forget();
        }
    }
}
