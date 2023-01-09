using Assets.Scripts.Messages;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Erntemaschine.Controllers
{
    internal class StartupController : MonoBehaviour
    {
        [Inject]
        private IMessageBus _messageBus;

        [Inject]
        private ResourcesStorage _resourcesStorage;

        private void Start()
        {
            _messageBus.Reset();
            _resourcesStorage.Clear();
            Restart().Forget();
        }

        private async UniTask Restart()
        {
            string[] scenes = { "Level1", "ShipEditorScene" };

            foreach (var scene in scenes)
            {
                if (SceneManager.GetSceneByName(scene).isLoaded)
                    await SceneManager.UnloadSceneAsync(scene).ToUniTask();
            }

            foreach (var scene in scenes)
            {
                await SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive).ToUniTask();
            }
        }
    }
}
