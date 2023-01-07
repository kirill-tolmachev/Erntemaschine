using System;

namespace Erntemaschine.Vehicles.Processors
{
    internal static class ProcessorExtensions
    {
        public static bool TryRead(this Func<Processor> processor, out float value)
        {
            if (processor == null)
            {
                value = default;
                return false;
            }

            var instance = processor();
            if (instance == null)
            {
                value = default;
                return false;
            }

            return instance.TryGetData(out value);
        }
    }

}
