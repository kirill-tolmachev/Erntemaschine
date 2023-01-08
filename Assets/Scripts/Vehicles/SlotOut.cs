using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Erntemaschine.Vehicles
{
    public class SlotOut : Slot
    {
        [SerializeField] private string _id;

        public string Id => _id;

        private HashSet<SlotIn> _listeners = new HashSet<SlotIn>();

        public IReadOnlyCollection<SlotIn> Listeners => _listeners;

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