using Assets.Scripts.Messages;
using Cysharp.Threading.Tasks;
using Erntemaschine.Messages.Impl;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Erntemaschine.Ui
{
    internal class ExitBuildMenuButton : MonoBehaviour, IInitializable
    {
        [SerializeField]
        private Button _button;

        [Inject]
        private IMessageBus _messageBus;

        private void Awake()
        {
            Initialize();
        }

        private void HideBuildMenu()
        {
            _messageBus.Publish(new HideBuildMenu()).Forget();
        }

        private void OnBuildMenuShown()
        {
            _button.gameObject.SetActive(true);
        }

        private void OnBuildMenuHidden()
        {
            _button.gameObject.SetActive(false);
        }

        public void Initialize()
        {
            _button.onClick.AddListener(HideBuildMenu);
            _messageBus.Subscribe<ShowBuildMenu>(x => OnBuildMenuShown());
            _messageBus.Subscribe<HideBuildMenu>(x => OnBuildMenuHidden());
        }
    }
}