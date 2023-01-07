using System.Collections.Generic;
using Assets.Scripts.Messages;
using Erntemaschine.Messages.Impl;
using UnityEngine;
using Zenject;

namespace Erntemaschine.Controllers
{
    internal class RadarController : MonoBehaviour
    {
        [SerializeField] 
        private GameObject _radarWave;

        [SerializeField]
        private float _speed;

        [SerializeField] 
        private float _maxScale = 30f;

        [Inject] 
        private DiContainer _container;

        [Inject] 
        private IMessageBus _messageBus;

        private readonly HashSet<SpriteRenderer> _existingWaves = new();

        private void Start()
        {
            _messageBus.Subscribe<RadarWaveStarted>(SpawnNewWave);
        }
        
        private void SpawnNewWave(RadarWaveStarted args)
        {
            var angle = Mathf.Atan2(args.Direction.y, args.Direction.x) - Mathf.PI / 2;
            var rotation = new Vector3(0f, 0f, angle * Mathf.Rad2Deg);
            var instance = _container.InstantiatePrefabForComponent<SpriteRenderer>(_radarWave, args.Origin, Quaternion.Euler(rotation), transform);
            _existingWaves.Add(instance);
        }

        public void Update()
        {
            var itemsToRemove = new List<SpriteRenderer>();

            foreach (var existingWave in _existingWaves)
            {
                var v = existingWave.transform.localScale.x + _speed * Time.deltaTime;
                existingWave.transform.localScale = new Vector3(v, v, v);

                float fadeout = 0.7f;

                if (v > _maxScale * fadeout)
                {
                    var c = existingWave.color;
                    var a = MathUtil.Renorm(v, _maxScale * fadeout, _maxScale);
                    existingWave.color = new Color(c.r, c.g, c.b, 1f - a);
                }

                if (v > _maxScale)
                {
                    itemsToRemove.Add(existingWave);
                }
            }

            foreach (var itemToRemove in itemsToRemove)
            {
                _existingWaves.Remove(itemToRemove);
                Destroy(itemToRemove.gameObject);
            }
        }
    }
}
