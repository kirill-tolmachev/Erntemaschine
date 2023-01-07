using System;
using UnityEngine;

namespace Erntemaschine.Vehicles.Processors
{
    internal class EngineProcessor : Processor
    {
        private Func<Processor> _input;

        [SerializeField]
        private ParticleSystem _particleSystem;

        private bool _isEnabled;

        void Start()
        {
            _input = UseSlot("input");
        }

        void Update()
        {
            if (!_input.TryRead(out var value))
            {
                _isEnabled = false;
                Toggle(false);
                return;
            }

            if (!_isEnabled)
            {
                Toggle(true);
            }
            

        }

        private void Toggle(bool value)
        {
            if (value)
            {
                _particleSystem.Play(true);
                _particleSystem.GetComponent<ParticleSystemRenderer>().enabled = true;
            }
            else
            {
                _particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            }
        }
    }
}
