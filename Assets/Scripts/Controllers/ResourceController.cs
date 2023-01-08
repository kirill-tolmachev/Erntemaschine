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
        [Inject]
        private IMessageBus _messageBus;

        [Inject] 
        private ResourcesStorage _storage;

        [SerializeField] private TMP_Text _wText;
        [SerializeField] private TMP_Text _xText;

        private void Start()
        {
            _messageBus.Subscribe<RewardGranted>(Callback);
            _messageBus.Subscribe<Harvest>(OnHarvest);
            _messageBus.Subscribe<ResourceSubtracted>(OnResourceSubtracted);
        }

        private void OnResourceSubtracted(ResourceSubtracted obj)
        {
            _storage.W -= obj.W;
            _storage.X -= obj.X;
        }

        private void Update()
        {
            _wText.text = _storage.W.ToMoneyString();
            _xText.text = _storage.X.ToMoneyString();
        }

        private void Callback(RewardGranted obj)
        {
            _storage.W += obj.Value;
        }

        private void OnHarvest(Harvest obj)
        {
            _storage.X += 100f;
        }
    }
}
