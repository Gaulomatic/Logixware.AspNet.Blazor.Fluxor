using System;

using Blazor.Fluxor;

namespace Logixware.Web.Blazor.Fluxor
{
	public interface IReactiveStore
	{
		IObservable<Object> States { get; }
		IObservable<IAction> Actions { get; }
	}
}
