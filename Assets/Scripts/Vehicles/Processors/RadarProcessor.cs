using System;
using Assets.Scripts.Messages;
using Cysharp.Threading.Tasks;
using Erntemaschine.Messages.Impl;
using UnityEngine;
using Zenject;

namespace Erntemaschine.Vehicles.Processors
{
    internal class RadarProcessor : Processor
    {
        [SerializeField] 
        private Lamp _lamp;

        [SerializeField]
        private float _tickInterval;

        private Color _defaultLampColor;
        private Func<Processor> _input;

        private float _lastTick;

        [Inject]
        private IMessageBus _messageBus;

        [SerializeField] 
        private Transform _direction;

        private void Start()
        {
            _input = UseSlot("input");
            _defaultLampColor = _lamp.Color;
        }

        private void Update()
        {
            var now = Time.time;
            if (!_input.TryRead(out var value))
            {
                _lamp.Toggle(false);
                _lastTick = now;
                return;
            }

            _lamp.Toggle(true);
            _lamp.Color = (value < 50 ? Color.red : _defaultLampColor);

            if (now - _lastTick > _tickInterval)
            {
                _lastTick = now;
                _messageBus.Publish(new RadarWaveStarted(transform.position, _direction.position - transform.position)).Forget();
            }
        }
    }
}
