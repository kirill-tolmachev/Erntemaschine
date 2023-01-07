using Erntemaschine.Vehicles;
using UnityEngine;
using Zenject;

namespace Erntemaschine.IoC
{
    [CreateAssetMenu(menuName = "GameInstaller")]
    internal class GameInstaller : ScriptableObjectInstaller<GameInstaller>
    {
        [SerializeField]
        private SlotColorProvider _slotColorProvider;

        [SerializeField] private Link _linkPrefab;

        public override void InstallBindings()
        {
            Container.Bind<SlotColorProvider>().FromInstance(_slotColorProvider).AsSingle();
            Container.Bind<Link>().WithId("prefab").FromInstance(_linkPrefab).AsSingle();

            Container.Bind<LinkDrawer>().ToSelf().AsSingle();
        }
    }
}
