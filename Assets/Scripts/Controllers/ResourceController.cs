using System.Globalization;
using Assets.Scripts.Messages;
using Erntemaschine.Messages;
using Erntemaschine.Messages.Impl;
using TMPro;
using UnityEngine;
using Zenject;

namespace Erntemaschine.Controllers
{
    internal class ResourceController : MonoBehaviour
    {
        private float _x;

        private float _w;

        [Inject]
        private IMessageBus _messageBus;

        [SerializeField] private TMP_Text _wText;
        [SerializeField] private TMP_Text _xText;

        private void Start()
        {
            _messageBus.Subscribe<RewardGranted>(Callback);
            _messageBus.Subscribe<Harvest>(OnHarvest);
        }

        private void Update()
        {
            _wText.text = _w.ToString("F", CultureInfo.InvariantCulture);
            _xText.text = _x.ToString("F", CultureInfo.InvariantCulture);
        }

        private void Callback(RewardGranted obj)
        {
            _w += obj.Value;
        }

        private void OnHarvest(Harvest obj)
        {
            _x += 100f;
        }
    }
}
