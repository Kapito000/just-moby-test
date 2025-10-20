using System;
using System.Collections.Generic;

namespace Common
{
	public class TypeLocator<TType> where TType : class
	{
		readonly Dictionary<Type, TType> _dictionary = new();

		public bool Contains<T>() =>
			_dictionary.ContainsKey(typeof(T));

		public TypeLocator<TType> Add(TType instance)
		{
			_dictionary[instance.GetType()] = instance;
			return this;
		}

		public TypeLocator<TType> Add<T>(IEnumerable<T> range)
			where T : class, TType
		{
			foreach (var item in range)
				Add(item);
			return this;
		}

		public TypeLocator<TType> AdAs<T>(TType instance)
			where T : class, TType
		{
			_dictionary[typeof(T)] = instance;
			return this;
		}

		public TypeLocator<TType> Remove<T>() where T : class, TType
		{
			_dictionary.Remove(typeof(T));
			return this;
		}

		public T Get<T>() where T : class, TType =>
			_dictionary[typeof(T)] as T;

		public bool TryGet<T>(out TType value) where T : class, TType =>
			_dictionary.TryGetValue(typeof(T), out value);
	}
}