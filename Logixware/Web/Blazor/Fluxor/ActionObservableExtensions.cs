using System;
using System.Reactive.Linq;

using Blazor.Fluxor;

namespace Logixware.Web.Blazor.Fluxor
{
	public static class ActionObservableExtensions
	{
		public static IObservable<TAction> TakeAction<TAction>(this IObservable<IAction> target) where TAction : IAction
		{
			return target

				.Where(action => action is TAction)
				.Select(action => (TAction) action);
		}

		public static IObservable<TState> TakeState<TState>(this IObservable<Object> target)
		{
			return target

				.Where(action => action is TState)
				.Select(action => (TState) action);
		}
	}
}
