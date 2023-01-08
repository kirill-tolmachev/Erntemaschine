using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Erntemaschine.Ui
{
    internal class RestartButton : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private void Start()
        {

            _button.onClick.AddListener(() => SceneManager.LoadScene(0));
        }

        void UnloadAllScenesExcept(string sceneName)
        {
            int c = SceneManager.sceneCount;
            for (int i = 0; i < c; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                print(scene.name);
                if (scene.name != sceneName)
                {
                    SceneManager.UnloadSceneAsync(scene);
                }
            }
        }
    }
}