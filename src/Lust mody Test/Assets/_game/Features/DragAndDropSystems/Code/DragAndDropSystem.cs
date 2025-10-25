using Features.Common;
using Features.DragAndDropSystems.ItemHolders;
using Features.DragAndDropSystems.ItemStartDrags;
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
		[Inject] ITowerItemDrag _towerItemDrag;
		[Inject] INewItemHolder _newItemHolder;
		[Inject] ITowerItemHolder _towerItemHolder;
		[Inject] IItemsDataCollectionProvider _itemsDataProvider;

		IDropProcessor _dropProcessor;
		IItemHolder _itemHolder;

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

			_towerItemDrag.DragItemStart
				.Subscribe(StartTowerItemDrag)
				.AddTo(this);

			_dropProcessor = new DropProcessor(_newItemPlacer);
		}

		void TryStartDrag(Vector2 screenPos)
		{
			_newItemDrag.TryDrag(screenPos);
		}

		void TryDrop(Vector2 screenPos)
		{
			_dropProcessor
				.SetScreenPos(screenPos)
				.SetDraggedItem(_draggedItem);

			_itemHolder.Accept(_dropProcessor);

			_dragSystem.Stop();
		}

		void StartNewItemDrag(INewItem newItem)
		{
			var id = newItem.Id;
			if (TryGetConfig(id, out var config) == false)
				return;

			_newItemHolder.Hold(id);
			_itemHolder = _newItemHolder;
			DragSystemStartDrag(config);
		}

		void StartTowerItemDrag(ITowerItem towerItem)
		{
			var id = towerItem.Id;
			if (TryGetConfig(id, out var config) == false)
				return;

			_towerItemHolder.Hold(true);
			_itemHolder = _towerItemHolder;
			DragSystemStartDrag(config);
		}

		bool TryGetConfig(string id, out IItemConfigDataProvider config)
		{
			if (_itemsDataProvider.TryGetConfig(id, out config) == false)
			{
				Debug.LogError($"Config not found: {id}");
				return false;
			}

			return true;
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