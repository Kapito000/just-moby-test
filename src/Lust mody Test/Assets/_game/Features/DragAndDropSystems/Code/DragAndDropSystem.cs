using Features.Common;
using Features.Input;
using Features.Items;
using Infrastructure;
using UniRx;
using UnityEngine;
using Zenject;

namespace Features.DragAndDropSystems
{
	public sealed class DragAndDropSystem : MonoBehaviour, IDragAndDropSystem,
		IBootEnter
	{
		static readonly Vector2 _spawnPos = new(-100, -100);

		[Inject] ISceneData _sceneData;
		[Inject] IDragSystem _dragSystem;
		[Inject] IItemFactory _itemFactory;
		[Inject] INewItemDrag _newItemDrag;
		[Inject] IBaseInputMap _baseInputMap;
		[Inject] INewItemPlacer _newItemPlacer;
		[Inject] IItemsDataCollectionProvider _itemsDataProvider;

		IItemHolder _itemHolder;
		INewItemHolder _newItemHolder = new NewItemHolder();

		Camera Camera => _sceneData.Camera;

		void IBootEnter.Execute()
		{
			_baseInputMap.StartDrag
				.Subscribe(TryStartDrag)
				.AddTo(this);
			_baseInputMap.Drop
				.Where(_ => _itemHolder is { IsHold: true })
				.Subscribe(TryDrop)
				.AddTo(this);
			_baseInputMap.PointerPosChanged
				.Where(_ => _itemHolder is { IsHold: true })
				.Subscribe(OnDragPhantomItem)
				.AddTo(this);

			_newItemDrag.DragItemStart
				.Subscribe(StartNewItemDrag)
				.AddTo(this);
		}

		void TryStartDrag(Vector2 screenPos)
		{
			_newItemDrag.TryDrag(screenPos);
		}

		void TryDrop(Vector2 screenPos)
		{
			switch (_itemHolder)
			{
				case INewItemHolder newItemHolder:
					_newItemPlacer.Place(screenPos, newItemHolder.Item);
					break;
				case ITowerItemHolder towerItemHolder:
					break;
			}

			_dragSystem.Stop();
		}

		void StartNewItemDrag(INewItem newItem)
		{
			var id = newItem.Id;
			if (_itemsDataProvider.TryGetConfig(id, out var config) == false)
			{
				Debug.LogError($"Config not found: {id}");
				return;
			}

			var item = _itemFactory.Create(_spawnPos, id);
			_newItemHolder.Hold(item);
			_itemHolder = _newItemHolder;
			DragSystemStartDrag(config);
		}

		void DragSystemStartDrag(IItemConfigDataProvider config)
		{
			var skin = config.Sprite;
			_dragSystem.StarDrag(skin);
		}

		void OnDragPhantomItem(Vector2 screenPos)
		{
			var pos = _sceneData.Camera.ScreenToWorldPoint(screenPos);
			_dragSystem.Drag(pos);
		}
	}
}