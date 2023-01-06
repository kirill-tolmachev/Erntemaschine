using System;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Erntemaschine
{
    public class TypewriterDisplay : TextDisplay
    {
        [SerializeField] private TMP_Text _text;

        [SerializeField] private int _charDelay;

        public override async UniTask DisplayText(string text)
        {
            _text.text = "";

            for (int i = 0; i < text.Length; i++)
            {
                if (ShouldDelayBeforeChar(text[i]))
                    await UniTask.Delay(_charDelay, DelayType.UnscaledDeltaTime);

                _text.text = text[..(i + 1)];
            }
        }

        private bool ShouldDelayBeforeChar(char ch) => !char.IsWhiteSpace(ch);
    }
}
