using System;
using UnityEngine;

namespace Erntemaschine.Vehicles.Processors
{
    internal class GeneratorProcessor : Processor
    {
        private Func<Processor[]> _out;

        private void Start()
        {
            _out = UseOutSlot("output");
        }

        public override bool TryGetData(out float data)
        {
            var outCount = _out().Length;
            data = Mathf.Sin(Time.time) / outCount;
            return true;
        }
    }
}
