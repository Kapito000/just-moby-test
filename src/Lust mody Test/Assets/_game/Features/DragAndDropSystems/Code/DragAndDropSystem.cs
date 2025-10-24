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
		[Inject] ISceneData _sceneData;
		[Inject] IDragSystem _dragSystem;
		[Inject] INewItemDrag _newItemDrag;
		[Inject] IDraggedItem _draggedItem;
		[Inject] IBaseInputMap _baseInputMap;
		[Inject] INewItemPlacer _newItemPlacer;
		[Inject] IItemsDataCollectionProvider _itemsDataProvider;

		IItemHolder _itemHolder;
		readonly INewItemHolder _newItemHolder = new NewItemHolder();

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
					_newItemPlacer.Place(screenPos, newItemHolder.Id, _draggedItem.Size);
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

			_newItemHolder.Hold(id);
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