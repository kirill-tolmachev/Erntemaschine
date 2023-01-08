using System.Collections.Generic;
using Assets.Scripts.Messages;
using Cysharp.Threading.Tasks;
using Erntemaschine.Controllers;
using Erntemaschine.Messages.Impl;
using Erntemaschine.Vehicles;
using Erntemaschine.Vehicles.Modules;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Erntemaschine.Ui
{
    internal class ModuleSelectionScreenController : MonoBehaviour, IInitializable
    {
        [SerializeField]
        private ModuleCategory[] _categories;

        [SerializeField]
        private ModuleCategoryUiItem _categoryPrefab;

        [SerializeField]
        private ModuleUiItem _itemPrefab;

        [SerializeField] 
        private RectTransform _categoriesContainer;

        [SerializeField]
        private RectTransform _itemContainer;

        [SerializeField] 
        private TMP_Text _title;

        [Inject]
        private DiContainer _container;

        [Inject]
        private IMessageBus _messageBus;

        [SerializeField]
        private CursorOverload _cursorOverload;

        [Inject] 
        private MapController _mapController;

        [SerializeField] 
        private Animator _animator;

        private readonly List<ModuleUiItem> _spawnedItems = new();

        public ModuleItem CurrentItem { get; private set; }

        private Part _placeholder;

        private void Start()
        {
           
        }

        private void Toggle(bool value)
        {
            gameObject.SetActive(value);
        }

        private void OnModuleConstructionAttempt(ModuleConstructionAttempt obj)
        {
            if (!obj.Item.Part.CanBeSpawned(obj.Position, obj.Rotation, _mapController))
                return;

            var instance = _container.InstantiatePrefabForComponent<Part>(obj.Item.Part, obj.Position,
                Quaternion.Euler(0f, 0f, obj.Rotation), MapRoot.Instance.transform);

            _mapController.AddSpawnedPart(instance);
            _messageBus.Publish(new ModuleButtonDeselected(obj.Item));
        }

        private void OnModuleDeselected(ModuleButtonDeselected obj)
        {
            CurrentItem = null;
            _cursorOverload.SetObject(null, null);
            Destroy(_placeholder.gameObject);

            _animator.SetBool("IsEnabled", true);
        }

        private void OnModuleSelected(ModuleButtonSelected obj)
        {
            CurrentItem = obj.Item;
            if (_placeholder != null)
            {
                Destroy(_placeholder.gameObject);
            }

            _placeholder = _container.InstantiatePrefabForComponent<Part>(obj.Item.Part);
            _placeholder.MakePlaceholder();
            _cursorOverload.SetObject(_placeholder.gameObject, obj.Item);

            _animator.SetBool("IsEnabled", false);
        }

        private void ChangeCategory(ModuleCategory category)
        {
            SelectItem(null);
            _title.text = category.CategoryName;

            for (int i = _spawnedItems.Count - 1; i >= 0; i--)
            {
                Destroy(_spawnedItems[i].gameObject);
                _spawnedItems.RemoveAt(i);
            }

            foreach (var item in category.Items)
            {
                var ui = _container.InstantiatePrefabForComponent<ModuleUiItem>(_itemPrefab, _itemContainer);
                ui.Init(item);
                ui.OnClick += () => SelectItem(ui.Item);

                _spawnedItems.Add(ui);
            }
        }

        private void SelectItem(ModuleItem item)
        {
            var old = CurrentItem;
            if (old != null)
            {
                _messageBus.Publish(new ModuleButtonDeselected(old)).Forget();
            }

            if (item != null && item != old)
            {
                _messageBus.Publish(new ModuleButtonSelected(item)).Forget();
            }
            
            CurrentItem = old == item ? null : item;
        }

        public void Initialize()
        {
            foreach (var category in _categories)
            {
                var instance = _container.InstantiatePrefabForComponent<ModuleCategoryUiItem>(_categoryPrefab, _categoriesContainer);
                instance.Init(category);
                instance.OnClick += () => ChangeCategory(category);
            }

            ChangeCategory(_categories[0]);

            _messageBus.Subscribe<ModuleButtonSelected>(OnModuleSelected);
            _messageBus.Subscribe<ModuleButtonDeselected>(OnModuleDeselected);
            _messageBus.Subscribe<ModuleConstructionAttempt>(OnModuleConstructionAttempt);
            _messageBus.Subscribe<HideBuildMenu>(x => Toggle(false));
            _messageBus.Subscribe<ShowBuildMenu>(x => Toggle(true));
        }
    }
}
