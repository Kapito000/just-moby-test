using System;
using Infrastructure;

namespace Features.SaveLoads
{
	public interface ISaveLoadService : IService
	{
		IObservable<Progress> SaveProgress { get; }
		IObservable<Progress> LoadProgress { get; }
		
		void Save();
		void Load();
	}
}