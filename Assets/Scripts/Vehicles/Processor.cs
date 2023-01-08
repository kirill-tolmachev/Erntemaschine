using System;
using System.Linq;
using UnityEngine;

namespace Erntemaschine.Vehicles
{
    public abstract class Processor : MonoBehaviour
    {
        protected Processor[] Inputs { get; private set; }

        public void Init(int inputCount)
        {
            Inputs = new Processor[inputCount];
        }

        protected Func<Processor> UseSlot(string id)
        {
            var slot = GetComponentsInChildren<SlotIn>().Single(x => x.Id == id);
            return () =>
            {
                var connection = slot.LinkedSlot;
                if (connection == null) return null;
                return connection.Processor;
            };
        }

        protected Func<Processor[]> UseOutSlot(string id)
        {
            var slot = GetComponentsInChildren<SlotOut>().Single(x => x.Id == id);
            return () =>
            {
                var listeners = slot.Listeners;
                return listeners.Select(x => x.Processor).ToArray();
            };
        }


        public virtual bool TryGetData(out float data)
        {
            data = 0f;
            return false;
        }
    }

}
