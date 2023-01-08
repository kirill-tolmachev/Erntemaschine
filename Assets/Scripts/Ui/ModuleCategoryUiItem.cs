using System;
using Erntemaschine.Vehicles.Modules;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Erntemaschine.Ui
{
    internal class ModuleCategoryUiItem : MonoBehaviour
    {
        public event Action OnClick;

        [SerializeField] 
        private Button _button;

        [SerializeField] 
        private TMP_Text _text;

        public void Init(ModuleCategory category)
        {
            _button.onClick.AddListener(() => OnClick?.Invoke());
            _text.text = category.CategoryName;
        }

    }
}