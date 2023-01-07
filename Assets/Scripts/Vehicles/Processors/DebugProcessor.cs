using System;
using System.Globalization;
using TMPro;
using UnityEngine;

namespace Erntemaschine.Vehicles.Processors
{
    internal class DebugProcessor : Processor
    {
        [SerializeField]
        private TMP_Text _output;

        private Func<Processor> Input { get; set; }

        void Start()
        {
            Input = UseSlot("input");
        }

        void Update()
        {
            _output.text = "N/A";
            if (!Input.TryRead(out var data))
                return;

            _output.text = data.ToString("F", CultureInfo.InvariantCulture);
        }
        
    }
}
