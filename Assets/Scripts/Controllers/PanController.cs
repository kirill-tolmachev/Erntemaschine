using Cinemachine;
using UnityEngine;

namespace Erntemaschine.Controllers
{
    internal class PanController : MonoBehaviour
    {
        [SerializeField]
        private float _mouseSensitivity = 1.0f;

        [SerializeField]
        private CinemachineVirtualCamera _camera;

        private Vector3 _lastPosition;

        private void Update()
        {
            if (Input.GetMouseButtonDown(2))
            {
                _lastPosition = Input.mousePosition;
            }

            if (Input.GetMouseButton(2))
            {
                var delta = Input.mousePosition - _lastPosition;
                _camera.transform.Translate(delta.x * _mouseSensitivity, delta.y * _mouseSensitivity, 0);
                _lastPosition = Input.mousePosition;
            }
        }

    }
}