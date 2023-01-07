﻿using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Erntemaschine.Vehicles.Processors
{
    internal class ControlBlockProcessor : Processor
    {
        [SerializeField] 
        private Button _button;

        [SerializeField]
        private TMP_Text _text;

        [SerializeField]
        private string _displayName;

        [SerializeField] 
        private Lamp _lamp;

        public bool Active { get; private set; }

        public string DisplayName
        {
            get => _text.text;
            set => _text.text = value;
        }

        private void Awake()
        {
            _button.onClick.AddListener(OnClick);
            DisplayName = _displayName;
        }

        private void OnClick()
        {
            Active = !Active;
            _lamp.Toggle(Active);
        }

        public override bool TryGetData(out float data)
        {
            if (!Active)
            {
                data = 0f;
                return false;
            }

            data = 1f;
            return true;
        }
    }
}