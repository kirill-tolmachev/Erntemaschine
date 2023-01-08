using Assets.Scripts.Messages;
using Cysharp.Threading.Tasks;
using Erntemaschine.Messages.Impl;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Erntemaschine.Ui
{
    internal class BuildMenuButton : MonoBehaviour
    {
        [SerializeField]
        private Button _button;

        [Inject]
        private IMessageBus _messageBus;

        private void Awake()
        {
            _button.onClick.AddListener(ShowBuildMenu);
            _messageBus.Subscribe<ShowBuildMenu>(x => OnBuildMenuShown());
            _messageBus.Subscribe<HideBuildMenu>(x => OnBuildMenuHidden());
        }

        private void ShowBuildMenu()
        {
            _messageBus.Publish(new ShowBuildMenu()).Forget();
        }

        private void OnBuildMenuShown()
        {
            _button.gameObject.SetActive(false);
        }

        private void OnBuildMenuHidden()
        {
            _button.gameObject.SetActive(true);
        }
    }
}
