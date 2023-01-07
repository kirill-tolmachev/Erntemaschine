using UnityEngine;

namespace Erntemaschine.Vehicles
{
    public class SlotIn : Slot
    {
        [SerializeField] 
        private string _id;

        public string Id => _id;

        public SlotOut LinkedSlot { get; set; }
    }
}