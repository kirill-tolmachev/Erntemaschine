using System.Collections.Generic;
using UnityEngine;

namespace Erntemaschine.Vehicles.Modules
{
    [CreateAssetMenu(menuName = "Module Category")]
    public class ModuleCategory : ScriptableObject
    {
        [SerializeField] private ModuleItem[] _items;
        public IReadOnlyList<ModuleItem> Items => _items;

        [SerializeField] private string _categoryName;
        public string CategoryName => _categoryName;

        [SerializeField] private Sprite _categoryImage;
        public Sprite CategoryImage => _categoryImage;
    }
}
