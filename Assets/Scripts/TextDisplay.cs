using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Erntemaschine
{
    public abstract class TextDisplay : MonoBehaviour
    {
        public abstract UniTask DisplayText(string text);
    }
}
