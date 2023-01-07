using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject.SpaceFighter;

namespace Erntemaschine.Ui
{
    internal class RetargetCanvas : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private float _planeDistance;

        void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            Retarget();
        }

        void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode) => Retarget();

        void Retarget()
        {
            var targetCamera = Camera.main;
            if (targetCamera == null)
            {
                Debug.LogWarning("Main camera not found");
                return;
            }

            _canvas.worldCamera = targetCamera;
            _canvas.planeDistance = _planeDistance;
        }


    }
}
