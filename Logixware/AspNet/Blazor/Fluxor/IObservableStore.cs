using System;

namespace Logixware.AspNet.Blazor.Fluxor
{
	/// <summary>
	/// Implements members to make the store observable.
	/// </summary>
	public interface IObservableStore
	{
		/// <summary>
		/// Returns an <see cref="T:System.IObservable`1" /> for the state objects inside the store.
		/// </summary>
		IObservable<object> States { get; }

		/// <summary>
		/// Returns an <see cref="T:System.IObservable`1" /> for actions which are dispatched.
		/// </summary>
		IObservable<object> Actions { get; }
	}
}
