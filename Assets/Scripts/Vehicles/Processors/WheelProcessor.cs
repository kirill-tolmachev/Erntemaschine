using System;
using UnityEngine;

namespace Erntemaschine.Vehicles.Processors
{
    internal class WheelProcessor : Processor
    {
        [SerializeField]
        private float _speed = 100f;
        private Func<Processor> _input;
        [SerializeField]
        private Rigidbody2D _wheel;

        void Start()
        {
            _input = UseSlot("input");
        }

        void FixedUpdate()
        {
            if (!_input.TryRead(out var value))
            {
                return;
            }

            float v = value * _speed * Time.fixedDeltaTime;
            _wheel.AddTorque(v);
        }
    }
}
