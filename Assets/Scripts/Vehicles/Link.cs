using UnityEngine;
using Zenject;

namespace Erntemaschine.Vehicles
{
    public class Link : MonoBehaviour
    {
        [Inject] 
        private LinkDrawer _drawer;

        [SerializeField] 
        private LineRenderer _lineRenderer;

        private bool _isEndBindedToCursor = false;

        private Slot _start;
        private Slot _end;

        public SlotIn SlotIn { get; private set; }
        public SlotOut SlotOut { get; private set; }

        private void Start()
        {
            _lineRenderer.positionCount = 2;
        }

        private void Update()
        {
            var start = _start;
            var end = (_isEndBindedToCursor ? GetWorldMousePosition() : _end.transform.position).WithZ(start.transform.position.z);

            _lineRenderer.SetPositions(new []{start.transform.position, end});

            if (Input.GetMouseButton(1))
                _drawer.DropLink();
        }

        private Vector3 GetWorldMousePosition()
        {
            var mp = Input.mousePosition;
            return Camera.main.ScreenToWorldPoint(mp);
        }

        public void BindEndToCursor()
        {
            _isEndBindedToCursor = true;
        }


        public void SetStart(Slot slot)
        {
            if (slot is SlotIn input)
                SlotIn = input;
            if (slot is SlotOut output)
                SlotOut = output;

            _start = slot;
        }

        public void SetEnd(Slot slot)
        {
            if (slot is SlotIn input)
                SlotIn = input;
            if (slot is SlotOut output)
                SlotOut = output;

            _end = slot;
            _isEndBindedToCursor = false;
        }

        public void SetColor(Color startColor, Color endColor)
        {
            _lineRenderer.startColor = startColor;
            _lineRenderer.endColor = endColor;
        }
    }
}