using System.Linq;
using Erntemaschine.Controllers;
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

        public void MakePlaceholder()
        {
            foreach (var child in GetComponentsInChildren<SpriteRenderer>())
            {
                child.color = new Color(child.color.r, child.color.g, child.color.b, 0.7f);
            }

            foreach (var slot in GetComponentsInChildren<Slot>())
            {
                slot.IsClickable = false;
            }
        }

        public bool CanBeSpawned(Vector3 position, float rotation, MapController map)
        {
            return map.SpawnedParts.All(x => !this.Intersects(x, position, rotation));
        }

        public bool Intersects(Part other, Vector3 thisPosition, float thisRotation)
        {
            return Vector3.Distance(thisPosition, other.transform.position) < 0.7f;
        }
    }
}
