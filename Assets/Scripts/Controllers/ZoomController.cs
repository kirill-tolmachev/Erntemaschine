using System;
using Cinemachine;
using UnityEngine;

namespace Erntemaschine.Controllers
{
    internal class ZoomController : MonoBehaviour
    {
        [SerializeField]
        private float _zoomSpeed;

        [SerializeField]
        private float _minZoom;
        
        [SerializeField]
        private float _maxZoom;

        [SerializeField] 
        private CinemachineVirtualCamera _mainCamera;

        private void Update()
        {
            float zoom = _mainCamera.m_Lens.OrthographicSize;
            zoom = Mathf.Clamp(zoom - Input.mouseScrollDelta.y * _zoomSpeed * Time.deltaTime, _minZoom, _maxZoom);
            _mainCamera.m_Lens.OrthographicSize = zoom;
        }
    }
}
