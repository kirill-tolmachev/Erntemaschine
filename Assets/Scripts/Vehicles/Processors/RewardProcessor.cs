using System;
using Assets.Scripts.Messages;
using Erntemaschine.Messages;
using Erntemaschine.Ui;
using UnityEngine;
using Zenject;

namespace Erntemaschine.Vehicles.Processors
{
    internal class RewardProcessor : Processor
    {
        private Func<Processor> _input;

        private float _progress;

        [SerializeField]
        private float _pointsToReward;

        [SerializeField] 
        private float _rewardValue;

        [SerializeField] 
        private float _modifier;

        [SerializeField] 
        private ProgressBar _progressBar;

        [Inject] 
        private IMessageBus _messageBus;

        private void Start()
        {
            _input = UseSlot("input");
        }

        private void Update()
        {
            if (!_input.TryRead(out var value))
            {
                return;
            }

            _progress += value * Time.deltaTime * _modifier;

            if (_progress > _pointsToReward)
            {
                _progress = 0;
                _messageBus.Publish(new RewardGranted(_rewardValue));
            }

            _progressBar.SetValue(_progress / _pointsToReward);
        }
    }
}
