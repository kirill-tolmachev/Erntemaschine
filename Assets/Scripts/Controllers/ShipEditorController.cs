using Erntemaschine.Ui;
using UnityEngine;

namespace Erntemaschine.Controllers
{
    internal class ShipEditorController : MonoBehaviour
    {
        [SerializeField] private ExitBuildMenuButton _exitBuildMenuButton;
        [SerializeField] private ModuleSelectionScreenController _selectionScreenController;

        private void Start()
        {
            _exitBuildMenuButton.Initialize();
            _selectionScreenController.Initialize();
        }
    }
}
