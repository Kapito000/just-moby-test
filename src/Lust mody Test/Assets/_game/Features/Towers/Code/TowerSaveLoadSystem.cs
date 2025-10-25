using System;
using System.Diagnostics;
using Features.SaveLoads;
using Infrastructure;
using UniRx;
using UnityEngine;
using Zenject;

namespace Features.Towers
{
	public sealed class TowerSaveLoadSystem : IBootEnter, IDisposable
	{
		[Inject] ITower _tower;
		[Inject] ISaveLoadService _saveLoadService;

		CompositeDisposable _compositeDisposable = new();

		void IBootEnter.Execute()
		{
			_saveLoadService
				.LoadProgress
				.Subscribe(OnLoadProgress)
				.AddTo(_compositeDisposable);
			_saveLoadService
				.SaveProgress
				.Subscribe(OnSaveProgress)
				.AddTo(_compositeDisposable);
		}

		void OnSaveProgress(Progress progress)
		{
			foreach (var placement in _tower.Placements)
			{
				progress.TowerPlacements.Add(new TowerPlacement()
				{
					Id = placement.Item.Id,
					Pos = placement.Pos,
				});
			}
		}

		void OnLoadProgress(Progress progress)
		{
			foreach (var progressPlacement in progress.TowerPlacements)
			{
				var id = progressPlacement.Id;
				var pos = progressPlacement.Pos;

				_tower.JustAddNext(new ItemPlaceData()
				{
					Id = id,
					Pos = pos,
				});
			}
		}

		public void Dispose()
		{
			_compositeDisposable?.Dispose();
		}
	}
}