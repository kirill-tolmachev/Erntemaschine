using System;
using Erntemaschine.Vehicles.Modules;
using UnityEngine;
using UnityEngine.UI;

namespace Erntemaschine.Ui
{
    internal class ModuleCategoryUiItem : MonoBehaviour
    {
        public event Action OnClick;

        [SerializeField] 
        private Button _button;

        public void Init(ModuleCategory category)
        {
            _button.onClick.AddListener(() => OnClick?.Invoke());
        }

    }
}