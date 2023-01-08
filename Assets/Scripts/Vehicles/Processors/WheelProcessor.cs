using System;
using UnityEngine;

namespace Erntemaschine.Vehicles.Processors
{
    internal class WheelProcessor : Processor
    {
        [SerializeField]
        private float _speed = 100f;
        private SlotReader _input;
        [SerializeField]
        private Rigidbody2D _wheel;

        protected override void Start()
        {
            base.Start();
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
