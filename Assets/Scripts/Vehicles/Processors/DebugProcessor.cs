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

        private SlotReader Input { get; set; }

        protected override void Start()
        {
            base.Start();
            Input = UseSlot("input");
        }

        protected override void Update()
        {
            base.Update();
            _output.text = "N/A";
            if (!Input.TryRead(out var data, 0))
                return;

            _output.text = data.ToString("F", CultureInfo.InvariantCulture);
        }
        
    }
}
