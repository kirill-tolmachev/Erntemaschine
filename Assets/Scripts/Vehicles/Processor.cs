using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

namespace Erntemaschine.Vehicles
{
    public abstract class Processor : MonoBehaviour
    {
        protected Processor[] Inputs { get; private set; }

        public float Overpower => _readers.Sum(x =>
        {
            if (!x.TryRead(out var v) || v <= x.Slot.MaxPowerIn)
                return 0f;

            return v - x.Slot.MaxPowerIn;
        });

        private Health _health;

        [SerializeField]
        private float _overpoweredHealthDecay = 5f;

        private Tweener _shaker;

        private readonly List<SlotReader> _readers = new();

        private Vector3 _initialPosition;

        public void Init(int inputCount)
        {
            Inputs = new Processor[inputCount];
        }

        protected virtual void Start()
        {
            _health = GetComponent<Health>();
            _initialPosition = transform.position;
        }

        protected virtual void Update()
        {
            if (Overpower > 0f)
            {
                _shaker ??= CreateShake();
                _shaker.ManualUpdate(Time.deltaTime, Time.unscaledDeltaTime);

                _health.Value -= Overpower / 5f * Time.deltaTime;
            }
            else
            {
                if (_shaker != null && _shaker.IsActive())
                {
                    _shaker.Kill();
                    _shaker = null;
                    transform.position = _initialPosition;
                }
            }
        }

        private Tweener CreateShake()
        {
            return DOTween.Shake(() => transform.position, v => transform.position = v.WithZ(transform.position.z), 1f, strength: 0.02f, vibrato: 15,
                fadeOut: false).SetUpdate(UpdateType.Manual).SetLoops(-1).Play();
        }

        protected SlotReader UseSlot(string id)
        {
            var origin = this;
            var slot = GetComponentsInChildren<SlotIn>().Single(x => x.Id == id);

            Processor GetProc()
            {
                var connection = slot.LinkedSlot;
                if (connection == null) return null;
                
                return connection.Processor;
            }

            var reader = new SlotReader(GetProc, slot);
            _readers.Add(reader);

            return reader;
        }

        protected Func<Processor[]> UseOutSlot(string id)
        {
            var slot = GetComponentsInChildren<SlotOut>().Single(x => x.Id == id);
            return () =>
            {
                var listeners = slot.Listeners;
                return listeners.Select(x => x.Processor).ToArray();
            };
        }


        public virtual bool TryGetData(out float data, int depth)
        {
            data = 0f;
            return false;
        }
    }

}
