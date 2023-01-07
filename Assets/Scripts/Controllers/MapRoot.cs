using UnityEngine;

namespace Erntemaschine.Controllers
{
    internal class MapRoot : MonoBehaviour
    {
        public static MapRoot Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }
    }
}
