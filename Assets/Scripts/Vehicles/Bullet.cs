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

        [Inject]
        private IMessageBus _messageBus;

        private void OnCollisionEnter2D(Collision2D col)
        {
            Debug.Log("Collision enter");
            var other = col.collider;
            if (!_targetMask.HasLayer(other.gameObject.layer))
                return;
            
            if (!other.gameObject.TryGetComponent(out Health health)) 
                return;

            health.Value -= Damage;

            var contact = col.GetContact(0);
            _messageBus.Publish(new BulletHit(this, contact.point)).Forget();
        }
    }
}
