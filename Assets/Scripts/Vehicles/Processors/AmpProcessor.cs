using System;
using Assets.Scripts.Messages;
using Erntemaschine.Messages.Impl;
using UnityEngine;
using Zenject;

namespace Erntemaschine.Vehicles.Processors
{
    internal class AmpProcessor : Processor
    {
        [SerializeField]
        private float _multiplier = 100f;
        private SlotReader _input;

        [Inject] private IMessageBus _messageBus;

        private Func<Processor[]> _output;

        protected override void Start()
        {
            base.Start();
            _input = UseSlot("input");
            _output = UseOutSlot("output");
        }
        
        public override bool TryGetData(out float data, int depth)
        {
            if (depth > 1000)
            {
                _messageBus.Publish(new ExplosionOccured(gameObject.transform.position));
                _messageBus.Publish(new ObjectDied(gameObject));
            }

            if (!_input.TryRead(out var value, depth+1))
            {
                data = 0.0f;
                return false;
            }

            data = value * _multiplier / _output().Length;
            return true;
        }
    }
}