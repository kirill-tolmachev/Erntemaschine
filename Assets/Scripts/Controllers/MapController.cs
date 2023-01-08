using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Messages;
using Erntemaschine.Messages.Impl;
using Erntemaschine.Vehicles;
using UnityEngine;
using Zenject;

namespace Erntemaschine.Controllers
{
    public class MapController : MonoBehaviour
    {
        public static MapController Instance { get; private set; }

        private HashSet<Part> _spawnedParts = new();

        [SerializeField]
        private Part[] _explicitParts;

        public IReadOnlyCollection<Part> SpawnedParts => _spawnedParts;

        public void AddSpawnedPart(Part part)
        {
            _spawnedParts.Add(part);
        }

        public void RemoveSpawnedPart(Part part)
        {
            _spawnedParts.Remove(part); ;

            foreach (var slotIn in part.SlotIns.ToArray())
            {
                _linkDrawer.DropAllLinks(slotIn);

                if (slotIn.LinkedSlot != null)
                    slotIn.LinkedSlot.Unlink(slotIn);
            }

            foreach (var slotOut in part.SlotOuts)
            {
                _linkDrawer.DropAllLinks(slotOut);

                foreach (var listener in slotOut.Listeners.ToArray())
                {
                    slotOut.Unlink(listener);
                }
            }

            Destroy(part.gameObject);
        }

        [Inject]
        private LinkDrawer _linkDrawer;

        [Inject]
        private IMessageBus _messageBus;

        public void Start()
        {
            Instance = this;
            _messageBus.Subscribe<ObjectDied>(OnObjectDied);
            
            foreach (var part in _explicitParts)
            {
                AddSpawnedPart(part);
            }
        }

        private void OnObjectDied(ObjectDied obj)
        {
            if (!obj.Object.TryGetComponent(out Part part))
                return;

            _messageBus.Publish(new ExplosionOccured(part.transform.position));
            RemoveSpawnedPart(part);
        }
    }
}
