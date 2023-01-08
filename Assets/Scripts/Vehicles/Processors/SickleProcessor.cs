using System;
using UnityEngine;

namespace Erntemaschine.Vehicles.Processors
{
    internal class SickleProcessor : Processor
    {
        private SlotReader _input;

        [SerializeField]
        private Transform _sickle;

        [SerializeField] private float _minAngle;
        [SerializeField] private float _maxAngle;

        private float _direction = 1f;

        protected override void Start()
        {
            base.Start();
            _input = UseSlot("input");
        }

        protected override void Update()
        {
            base.Update();
            if (!_input.TryRead(out var value))
            {
                return;
            }

            var rotation = _sickle.localRotation.eulerAngles.z;
            rotation = MathUtil.NormalizeAngle(rotation);

            if (rotation < _minAngle || rotation > _maxAngle)
            {
                _direction *= -1f;
            }

            rotation += _direction * value * Time.deltaTime;
            _sickle.localRotation = Quaternion.Euler(0f, 0f, rotation);
        }
    }
}
