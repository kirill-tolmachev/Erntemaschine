using UnityEngine;

namespace Erntemaschine.Vehicles.Processors
{
    internal class GeneratorProcessor : Processor
    {
        public override bool TryGetData(out float data)
        {
            data = Mathf.Sin(Time.time);
            return true;
        }
    }
}
