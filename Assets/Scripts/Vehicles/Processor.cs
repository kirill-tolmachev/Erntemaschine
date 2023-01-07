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


        public virtual bool TryGetData(out float data)
        {
            data = 0f;
            return false;
        }
    }

}
