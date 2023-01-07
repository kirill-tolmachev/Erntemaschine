using System;
using UnityEngine;

namespace Erntemaschine.Vehicles.Processors
{
    internal class AmpProcessor : Processor
    {
        [SerializeField]
        private float _multiplier = 100f;
        private Func<Processor> _input;

        void Start()
        {
            _input = UseSlot("input");
        }
        
        public override bool TryGetData(out float data)
        {
            if (!_input.TryRead(out var value))
            {
                data = 0.0f;
                return false;
            }

            data = value * _multiplier;
            return true;
        }
    }
}