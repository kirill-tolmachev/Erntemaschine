using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Erntemaschine
{
    internal class Example : MonoBehaviour
    {
        [SerializeField] TextDisplay _display;

        public void Start()
        {
            _display.DisplayText("Please do this for me").Forget();
        }
    }
}
