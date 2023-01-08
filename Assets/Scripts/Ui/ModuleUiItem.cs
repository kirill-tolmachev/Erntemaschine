using System;
using System.Globalization;
using Assets.Scripts.Messages;
using Erntemaschine.Controllers;
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

        [SerializeField] 
        private TMP_Text _xText;

        [SerializeField] 
        private RectTransform _xParent;

        [SerializeField]
        private TMP_Text _wText;

        [SerializeField]
        private RectTransform _wParent;

        [Inject]
        private ResourcesStorage _resources;

        [SerializeField] 
        private RectTransform _grayOut;

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

            _xText.text = Math.Round(item.CostX).ToString(CultureInfo.InvariantCulture);
            _xParent.gameObject.SetActive(item.CostX > 0.1f);
            _wText.text = Math.Round(item.CostW).ToString(CultureInfo.InvariantCulture);
            _wParent.gameObject.SetActive(item.CostW > 0.1f);

            _messageBus.Subscribe<ModuleButtonSelected>(OnModuleButtonSelected);
            _messageBus.Subscribe<ModuleButtonDeselected>(OnModuleButtonDeselected);
        }

        private void Update()
        {
            if (Item == null) 
                return;

            bool canAfford = _resources.W >= Item.CostW && _resources.X >= Item.CostX;
            _button.interactable = canAfford;
            _grayOut.gameObject.SetActive(!canAfford);
        }

        private void OnModuleButtonSelected(ModuleButtonSelected message)
        {
            if (message.Item != Item)
                return;

            foreach (var frame in _frames)
            {
                if (frame != null)
                {
                    frame.color = Color.red;
                }
            }
        }

        private void OnModuleButtonDeselected(ModuleButtonDeselected message)
        {
            if (message.Item != Item)
                return;

            foreach (var frame in _frames)
            {
                if (frame != null)
                {
                    frame.color = Color.black;
                }
            }
        }
    }
}