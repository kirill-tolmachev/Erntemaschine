using System.Collections.Generic;
using UnityEngine;

namespace Erntemaschine.Vehicles
{
    public class SlotOut : Slot
    {
        private List<SlotIn> _listeners = new List<SlotIn>();

        public override bool IsOutput => true;

        public void Link(SlotIn slot)
        {
            _listeners.Add(slot);
            slot.LinkedSlot = this;
        }

        public void Unlink(SlotIn slot)
        {
            _listeners.Remove(slot);
        }

        
    }
}