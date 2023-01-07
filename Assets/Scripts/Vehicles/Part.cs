using UnityEngine;

namespace Erntemaschine.Vehicles
{
    public class Part : MonoBehaviour
    {
        public SlotIn[] SlotIns { get; private set; }

        public SlotOut[] SlotOuts { get; private set; }

        public Processor Processor { get; private set; }


        private void Awake()
        {
            SlotIns = GetComponentsInChildren<SlotIn>();
            SlotOuts = GetComponentsInChildren<SlotOut>();
            Processor = GetComponent<Processor>();
        }
    }
}
