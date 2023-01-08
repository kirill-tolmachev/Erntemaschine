using System;
using UnityEngine;

namespace Erntemaschine.Vehicles.Processors
{
    internal class GeneratorProcessor : Processor
    {
        private Func<Processor[]> _out;

        protected override void Start()
        {
            base.Start();
            _out = UseOutSlot("output");
        }

        public override bool TryGetData(out float data, int depth)
        {
            var outCount = _out().Length;
            data = Mathf.Sin(Time.time) / outCount;
            return true;
        }
    }
}
