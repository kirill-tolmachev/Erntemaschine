using Erntemaschine.Sounds;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Erntemaschine.Ui
{
    [RequireComponent(typeof(Button))]
    internal class UiButton: MonoBehaviour
    {
        [Inject(Id = "buttons")] private RandomSoundPlayer _soundPlayer;

        public void Awake()
        {
            var button = GetComponent<Button>();
            button.onClick.AddListener(PlaySound);
        }

        private void PlaySound() => _soundPlayer.Play();
    }
}
