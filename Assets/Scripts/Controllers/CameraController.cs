using Assets.Scripts.Messages;
using Cinemachine;
using Erntemaschine.Messages.Impl;
using UnityEngine;
using Zenject;

namespace Erntemaschine.Controllers
{
    internal class CameraController : MonoBehaviour
    {
        [Inject] 
        private IMessageBus _messageBus;

        [SerializeField] 
        private CinemachineVirtualCamera _virtualCamera;

        private float _shakeTimer = 0f;
        private float _shakeTimerTotal = 0f;
        private float _startingIntensity = 0f;

        private void Awake()
        {
            _messageBus.Subscribe<GunShot>(_ => CameraShake(5f, 0.5f));
        }

        private void Update()
        {
            if (_shakeTimer > 0f)
            {
                _shakeTimer -= Time.deltaTime;
                var perlin = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                perlin.m_AmplitudeGain = Mathf.Lerp(_startingIntensity, 0f, (1 - _shakeTimer / _shakeTimerTotal)); 
            }
        }

        private void CameraShake(float intensity, float time)
        {
            var perlin = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            perlin.m_AmplitudeGain = intensity;

            _startingIntensity = intensity;
            _shakeTimerTotal = time;
            _shakeTimer = time;
        }
    }
}
