using Assets.Scripts.Messages;
using Cysharp.Threading.Tasks;
using Erntemaschine.Messages.Impl;
using UnityEngine;
using Zenject;

namespace Erntemaschine.Vehicles
{
    internal class Health : MonoBehaviour
    {
        [Inject] 
        private IMessageBus _messageBus;

        [SerializeField]
        private float _value;

        [SerializeField]
        private float _maxValue;

        private bool _isAlive = true;

        public float Value
        {
            get => _value;
            set
            {
                OnValueChanging(ref value);
                _value = value;
            }
        }

        private void OnValueChanging(ref float newValue)
        {
            if (_isAlive && newValue <= 0f)
            {
                _messageBus.Publish(new ObjectDied(this.gameObject)).Forget();
                _isAlive = false;
            }

            newValue = Mathf.Clamp(newValue, 0f, _maxValue);
        }
    }
}
