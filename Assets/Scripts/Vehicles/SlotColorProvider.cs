using System;
using UnityEngine;

namespace Erntemaschine.Vehicles 
{
    [CreateAssetMenu(fileName = "Slot Color Provider")]
    internal class SlotColorProvider : ScriptableObject
    {
        public Color BoolColor;

        public Color FloatColor;

        public Color Provide(SlotType type, bool isOutput)
        {
            if (type == SlotType.Float)
            {
                return isOutput ? BoolColor : FloatColor;
            }

            throw new NotImplementedException();
        }
    }
}
