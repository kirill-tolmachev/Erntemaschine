using UnityEngine;
using UnityEngine.UI;

namespace Erntemaschine.Ui
{
    internal class ProgressBar : MonoBehaviour
    {
        [SerializeField] private Slider _slider;


        public void SetValue(float value)
        {
            _slider.value = value;
        }
    }
}
