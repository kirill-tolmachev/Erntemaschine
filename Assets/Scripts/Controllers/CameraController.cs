using Assets.Scripts.Messages;
using Erntemaschine.Messages.Impl;
using UnityEngine;
using Zenject;

namespace Erntemaschine.Controllers
{
    internal class CameraController : MonoBehaviour
    {
        [Inject] 
        private IMessageBus _messageBus;

        private void Awake()
        {
            _messageBus.Subscribe<GunShot>(_ => CameraShake(15f));
        }

        private void CameraShake(float intensity)
        {

        }
    }
}
