using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace Erntemaschine.Vehicles
{
    public abstract class Slot : MonoBehaviour
    {
        [Inject] 
        private SlotColorProvider _colorProvider;

        [Inject] 
        private LinkDrawer _linkDrawer;

        [SerializeField]
        private SlotType _type;
        public SlotType Type => _type;

        [SerializeField] private bool _isClickable = true;
        public bool IsClickable
        {
            get => _isClickable;
            set => _isClickable = value;
        }

        public Processor Processor { get; private set; }

        public virtual bool IsOutput => false;

        public void Start()
        {
            var button = GetComponentInChildren<Button>();
            button.onClick.AddListener(() => OnClick());

            var image = GetComponent<SpriteRenderer>();
            image.color = _colorProvider.Provide(Type);

            Processor = GetComponentInParent<Processor>();
        }

        public void OnClick()
        {
            Debug.Log("Click");
            if (!IsClickable)
                return;

            if (_linkDrawer.CurrentLink == null)
                _linkDrawer.StartLink(this);
            else
                _linkDrawer.TryEndLink(this);
        }
    }
}