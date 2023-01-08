using Erntemaschine.Controllers;
using Erntemaschine.Sounds;
using UnityEngine;
using Zenject;

namespace Erntemaschine.IoC
{
    internal class GameSceneInstaller : MonoInstaller
    {
        [SerializeField] 
        private RandomSoundPlayer _buttonSounds;

        [SerializeField]
        private RadarController _radarController;

        [SerializeField]
        private EnemyController _enemyController;

        [SerializeField]
        private MapController _mapController;

        public override void InstallBindings()
        {
            Container.Bind<RandomSoundPlayer>().WithId("buttons").FromInstance(_buttonSounds).AsSingle();
            Container.Bind<RadarController>().FromInstance(_radarController).AsSingle();
            Container.Bind<EnemyController>().FromInstance(_enemyController).AsSingle();
            Container.Bind<MapController>().FromInstance(_mapController).AsSingle();
        }
    }
}
