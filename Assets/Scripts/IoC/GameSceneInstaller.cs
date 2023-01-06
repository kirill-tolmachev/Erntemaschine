using Erntemaschine.Sounds;
using UnityEngine;
using Zenject;

namespace Erntemaschine.IoC
{
    internal class GameSceneInstaller : MonoInstaller
    {
        [SerializeField] 
        private RandomSoundPlayer _buttonSounds;

        public override void InstallBindings()
        {
            Container.Bind<RandomSoundPlayer>().WithId("buttons").FromInstance(_buttonSounds).AsSingle();
        }
    }
}
