using System;
using System.Reactive.Linq;

using Blazor.Fluxor;

namespace Logixware.AspNet.Blazor.Fluxor
{
	/// <summary>
	/// Extensions for Observables
	/// </summary>
	public static class ObservableExtensions
	{
		/// <summary>
		///	Returns an <see cref="T:System.IObservable`1" /> filtered for the given <see cref="T:Blazor.Fluxor.IAction" />
		/// </summary>
		/// <param name="target">The source <see cref="T:System.IObservable`1" /></param>
		/// <typeparam name="TAction"><see cref="T:Blazor.Fluxor.IAction" /> to filter</typeparam>
		/// <returns>The <see cref="T:System.IObservable`1" /> filtered for the given <see cref="T:Blazor.Fluxor.IAction" /></returns>
		/// <exception cref="ArgumentNullException">If target is null</exception>
		public static IObservable<TAction> TakeAction<TAction>(this IObservable<IAction> target) where TAction : IAction
		{
			if (target == null) throw new ArgumentNullException(nameof(target));

			return target

				.Where(action => action is TAction)
				.Select(action => (TAction) action);
		}

		/// <summary>
		/// Returns an <see cref="T:System.IObservable`1" /> filtered for the given state type
		/// </summary>
		/// <param name="target">The source <see cref="T:System.IObservable`1" /></param>
		/// <typeparam name="TState">The state type</typeparam>
		/// <returns>The <see cref="T:System.IObservable`1" /> filtered for the given state type</returns>
		/// <exception cref="ArgumentNullException">If target is null</exception>
		public static IObservable<TState> TakeState<TState>(this IObservable<Object> target)
		{
			if (target == null) throw new ArgumentNullException(nameof(target));

			return target

				.Where(action => action is TState)
				.Select(action => (TState) action);
		}
	}
}
