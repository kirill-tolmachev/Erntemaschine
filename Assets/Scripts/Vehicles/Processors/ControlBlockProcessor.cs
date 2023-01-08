using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Erntemaschine.Vehicles.Processors
{
    internal class ControlBlockProcessor : Processor
    {
        private static char _names;

        [SerializeField] 
        private Button _button;

        [SerializeField]
        private TMP_Text _text;

        [SerializeField]
        private string _displayName;

        [SerializeField] 
        private Lamp _lamp;

        public bool Active { get; private set; }

        private Func<Processor[]> _out;

        public string DisplayName
        {
            get => _text.text;
            set => _text.text = value;
        }

        private void Awake()
        {
            _button.onClick.AddListener(OnClick);
            DisplayName = Alphabet[Random.Range(0, Alphabet.Length)].ToString().ToUpperInvariant();
            _out = UseOutSlot("output");
        }

        private void OnClick()
        {
            Active = !Active;
            _lamp.Toggle(Active);
        }

        public override bool TryGetData(out float data, int depth)
        {
            if (!Active)
            {
                data = 0f;
                return false;
            }

            var outCount = _out().Length;


            data = 1f / outCount;
            return true;
        }

        private string Alphabet = "abcdefghijklmnopqrstuvwxyz";

    }
}
