using System;

using Blazor.Fluxor;

namespace Logixware.Web.Blazor.Fluxor
{
	public interface IReactiveEffects
	{
		IObservable<IAction> Actions { get; }
	}
}
