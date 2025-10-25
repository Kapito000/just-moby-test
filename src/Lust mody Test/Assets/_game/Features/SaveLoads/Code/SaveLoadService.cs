using System;
using System.Diagnostics;
using System.IO;
using UniRx;
using UnityEngine;

namespace Features.SaveLoads
{
	public sealed class SaveLoadService : ISaveLoadService, IDisposable
	{
		const string _fileName = "Saves.json";

		readonly Subject<Progress> _saveProgress = new();
		readonly Subject<Progress> _loadProgress = new();

		public IObservable<Progress> SaveProgress => _saveProgress;
		public IObservable<Progress> LoadProgress => _loadProgress;

		string FilePath =>
			Path.Combine(Application.persistentDataPath, _fileName);

		public SaveLoadService()
		{
#if UNITY_EDITOR
			Application.quitting += OnQuitting;
			Application.focusChanged += OnFocusChanged;
#endif
		}

		public void Save()
		{
			var progress = new Progress();
			_saveProgress.OnNext(progress);

			var json = JsonUtility.ToJson(progress, true);
			File.WriteAllText(FilePath, json);
		}

		public void Load()
		{
			if (File.Exists(FilePath) == false)
				return;

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

#if UNITY_EDITOR
		void OnQuitting()
		{
			Application.quitting -= OnQuitting;
			Application.focusChanged -= OnFocusChanged;

			Save();
		}

		void OnFocusChanged(bool hasFocus)
		{
			if (!hasFocus)
				Save();
		}
#endif
	}
}