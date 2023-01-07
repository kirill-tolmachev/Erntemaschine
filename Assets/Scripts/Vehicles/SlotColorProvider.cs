using System;
using UnityEngine;

namespace Erntemaschine.Vehicles 
{
    [CreateAssetMenu(fileName = "Slot Color Provider")]
    internal class SlotColorProvider : ScriptableObject
    {
        public Color BoolColor;

        public Color FloatColor;

        public Color Provide(SlotType type)
        {
            switch (type)
            {
                case SlotType.Bool:
                    return BoolColor;
                case SlotType.Float:
                    return FloatColor;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}
