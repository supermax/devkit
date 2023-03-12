using System;

namespace DevKit.Core.Observables
{
	/// <summary>
	///     Singleton Base Class
	/// </summary>
	/// <typeparam name="T">Type of Singleton</typeparam>
	public class ObservableSingleton<T>
		: Observable<T>
		where T : class
		, IObservable<T>
		, new()
	{
		public static T Default { get; } = new T();
	}
}
