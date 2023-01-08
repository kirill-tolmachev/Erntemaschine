using System;

namespace Erntemaschine.Vehicles
{
    public class SlotReader
    {
        private Func<Processor> GetProcessor { get; }

        public SlotIn Slot { get; }

        public SlotReader(Func<Processor> getProcessor, SlotIn slot)
        {
            GetProcessor = getProcessor;
            Slot = slot;
        }

        public bool TryRead(out float data, int depth = 0)
        {
            if (GetProcessor == null)
            {
                data = default;
                return false;
            }

            var instance = GetProcessor();
            if (instance == null)
            {
                data = default;
                return false;
            }

            bool result = instance.TryGetData(out data, depth+1);
            
            return result;
        }
    }
}