using UnityEngine;

namespace Erntemaschine.Vehicles.Modules
{
    [CreateAssetMenu(menuName = "Module Item")]
    public class ModuleItem : ScriptableObject
    {
        [SerializeField] private string _itemName;
        public string Name => _itemName;

        [SerializeField] private string _itemDescription;
        public string Description => _itemDescription;

        [SerializeField] private Sprite _sprite;
        public Sprite Sprite => _sprite;

        [SerializeField] private Part _part;
        public Part Part => _part;
    }
}