using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Erntemaschine.Controllers
{
    internal class StartupController : MonoBehaviour
    {
        private void Start()
        {
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
