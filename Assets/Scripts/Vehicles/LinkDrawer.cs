using System.Collections.Generic;
using System.Linq;
using Erntemaschine.Controllers;
using UnityEngine;
using Zenject;

namespace Erntemaschine.Vehicles
{
    internal class LinkDrawer
    {
        [Inject] private SlotColorProvider _colorProvider;

        [Inject(Id = "prefab")] private Link _lineRendererPrefab;

        [Inject] private DiContainer _container;
        
        public Link CurrentLink { get; private set; }

        public Slot StartSlot { get; private set; }

        private readonly HashSet<Link> _links = new HashSet<Link>();

        public Link StartLink(Slot slot)
        {
            if (CurrentLink != null)
                return null;

            StartSlot = slot;
            CurrentLink = _container.InstantiatePrefab(_lineRendererPrefab, MapRoot.Instance.transform).GetComponent<Link>();
            CurrentLink.SetStart(slot);
            var myColor = _colorProvider.Provide(slot.Type, slot.IsOutput);
            var otherColor = _colorProvider.Provide(slot.Type, !slot.IsOutput);
            CurrentLink.SetColor(myColor, otherColor);
            CurrentLink.BindEndToCursor();

            if (slot is SlotIn input)
            {
                DropExistingInputLink(input);
            }

            return CurrentLink;
        }

        public bool TryEndLink(Slot slot)
        {
            if (slot.Type != StartSlot.Type)
                return false;
            
            if (StartSlot.IsOutput == slot.IsOutput)
                return false;

            if (CurrentLink == null) 
                return false;

            GetInOut(StartSlot, slot, out var input, out var output);
            DropExistingInputLink(input);

            CurrentLink.SetEnd(slot);
            _links.Add(CurrentLink);

            output.Link(input);

            CurrentLink = null;
            return true;
        }

        public void DropAllLinks(SlotIn slot) => DropExistingInputLink(slot);

        public void DropAllLinks(SlotOut slot)
        {
            var links = _links.Where(x => x.SlotOut == slot).ToList();

            foreach (var link in links)
            {
                if (link.SlotIn != null)
                    link.SlotIn.LinkedSlot = null;

                DropLink(link);
            }
        }

        private void DropExistingInputLink(SlotIn input)
        {
            if (input.LinkedSlot != null)
            {
                var link = _links.First(x => x.SlotIn == input);
                DropLink(link);
                input.LinkedSlot = null;
            }
        }

        private void GetInOut(Slot x, Slot y, out SlotIn slotIn, out SlotOut slotOut)
        {
            slotIn = (SlotIn)(!x.IsOutput ? x : y);
            slotOut = (SlotOut)(x.IsOutput ? x : y);
        }

        public void DropLink()
        {
            DropLink(CurrentLink);
            CurrentLink = null;
        }

        private void DropLink(Link link)
        {
            _links.Remove(link);
            if (link != null)
            {
                Object.Destroy(link.gameObject);
            }
        }
    }
}
