using System;
using System.IO;
using UniRx;
using UnityEngine;

namespace Features.SaveLoads
{
	public sealed class SaveLoadService : ISaveLoadService, IDisposable
	{
		const string _fileName = "Saves.json";

		Subject<Progress> _saveProgress = new();
		Subject<Progress> _loadProgress = new();

		public IObservable<Progress> SaveProgress => _saveProgress;
		public IObservable<Progress> LoadProgress => _loadProgress;

		string FilePath =>
			Path.Combine(Application.persistentDataPath, _fileName);

		public void Save()
		{
			var progress = new Progress();
			_saveProgress.OnNext(progress);

			var json = JsonUtility.ToJson(progress, true);
			File.WriteAllText(FilePath, json);
		}

		public void Load()
		{
			var json = File.ReadAllText(FilePath);
			var progress = JsonUtility.FromJson<Progress>(json);

			_loadProgress.OnNext(progress);
		}

		public void Dispose()
		{
			_saveProgress.OnCompleted();
			_loadProgress.OnCompleted();

			_saveProgress?.Dispose();
			_loadProgress?.Dispose();
		}
	}
}