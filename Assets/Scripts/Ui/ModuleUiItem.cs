using System;
using Assets.Scripts.Messages;
using Erntemaschine.Messages.Impl;
using Erntemaschine.Vehicles.Modules;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Erntemaschine.Ui
{
    internal class ModuleUiItem : MonoBehaviour
    {
        public event Action OnClick;

        [SerializeField]
        private Button _button;

        [SerializeField] 
        private Image _image;

        [SerializeField] 
        private TMP_Text _title;

        [SerializeField]
        private TMP_Text _description;

        [SerializeField]
        private Image[] _frames;

        public ModuleItem Item { get; private set; }

        [Inject]
        private IMessageBus _messageBus;

        public void Init(ModuleItem item)
        {
            Item = item;
            _image.sprite = item.Sprite;
            _title.text = item.Name;
            _description.text = item.Description;
            _button.onClick.AddListener(() => OnClick?.Invoke());

            _messageBus.Subscribe<ModuleButtonSelected>(OnModuleButtonSelected);
            _messageBus.Subscribe<ModuleButtonDeselected>(OnModuleButtonDeselected);
        }

        private void OnModuleButtonSelected(ModuleButtonSelected message)
        {
            if (message.Item != Item)
                return;

            foreach (var frame in _frames)
            {
                frame.color = Color.red;
            }
        }

        private void OnModuleButtonDeselected(ModuleButtonDeselected message)
        {
            if (message.Item != Item)
                return;

            foreach (var frame in _frames)
            {
                frame.color = Color.black;
            }
        }
    }
}