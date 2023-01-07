using Assets.Scripts.Messages;
using Cysharp.Threading.Tasks;
using Erntemaschine.Messages.Impl;
using UnityEngine;
using Zenject;

namespace Erntemaschine.Vehicles.Modules
{
    internal class CursorOverload : MonoBehaviour
    {
        private GameObject _object;

        private ModuleItem _item;

        public bool SnapToGrid { get; set; } = true;

        [Inject] 
        private IMessageBus _messageBus;

        void Update()
        {
            if (_object != null)
            {
                _object.transform.position = GetObjectPosition();

                if (Input.GetKeyDown(KeyCode.E))
                {
                    Rotate(-90);
                }

                if (Input.GetKeyDown(KeyCode.Q))
                {
                    Rotate(90);
                }

                if (Input.GetMouseButtonDown(0))
                {
                    _messageBus.Publish(new ModuleConstructionAttempt(_item, _object.transform.position, _object.transform.rotation.eulerAngles.z)).Forget();
                }

                if (Input.GetMouseButtonDown(1))
                {
                    _messageBus.Publish(new ModuleButtonDeselected(_item)).Forget();
                }
            }
        }

        private void Rotate(float angle)
        {
            var old = _object.transform.rotation.eulerAngles;
            _object.transform.rotation = Quaternion.Euler(old.WithZ(old.z + angle));
        }

        public void SetObject(GameObject obj, ModuleItem item)
        {
            _object = obj;
            _item = item;
            Cursor.visible = obj == null;
        }

        Vector3 GetObjectPosition()
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (!SnapToGrid)
                return mousePosition;

            return Snapping.Snap(mousePosition, new Vector2(1f, 1f));
        }
    }
}
