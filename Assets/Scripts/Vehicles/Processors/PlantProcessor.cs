using System;
using Assets.Scripts.Messages;
using Cysharp.Threading.Tasks;
using Erntemaschine.Messages.Impl;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Erntemaschine.Vehicles.Processors
{
    internal class PlantProcessor : Processor
    {
        private SlotReader _input;

        [SerializeField]
        private float _minScale;

        [SerializeField]
        private float _maxScale;

        [SerializeField]
        private Button _button;

        [SerializeField] 
        private float _growthSpeed;

        [SerializeField] 
        private Color _disabledColor;

        [SerializeField] 
        private Color _enabledColor;

        private float _value;

        [Inject]
        private IMessageBus _messageBus;

        protected override void Start()
        {
            base.Start();
            _input = UseSlot("input");
            _button.onClick.AddListener(OnHarvest);
        }

        protected override void Update()
        {
            base.Update();
            bool buttonActive = _value >= 1f;
            _button.interactable = buttonActive;
            var scale = _minScale + (_maxScale - _minScale) * _value;
            _button.transform.localScale = new Vector3(scale, scale, 1f);
            _button.image.color = buttonActive ? _enabledColor : _disabledColor;

            if (!_input.TryRead(out var value))
                return;

            _value = Mathf.Clamp(_value + value * _growthSpeed * Time.deltaTime, _minScale, _maxScale);
        }

        private void OnHarvest()
        {
            _value = 0f;
            _messageBus.Publish(new Harvest()).Forget();
        }
    }
}
