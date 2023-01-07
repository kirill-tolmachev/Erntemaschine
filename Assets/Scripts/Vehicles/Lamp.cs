using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Erntemaschine.Vehicles
{
    internal class Lamp : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        [SerializeField]
        private Light2D _light;

        public Color Color
        {
            get => _light.color;
            set => _light.color = value;
        }

        void Awake()
        {
            _light.color = _spriteRenderer.color;
            Toggle(false);
        }

        public void Toggle(bool v)
        {
            _spriteRenderer.enabled = !v;
            float intensity = v ? 20f : 2f;
            _light.intensity = intensity;
        }

    }
}
