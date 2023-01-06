using UnityEngine;

namespace Erntemaschine
{
    [CreateAssetMenu(fileName = "SoundCollection")]
    internal class SoundCollection : ScriptableObject
    {
        public AudioClip[] Clips;
    }
}
