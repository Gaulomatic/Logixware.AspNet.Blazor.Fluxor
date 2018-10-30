using System;

namespace Logixware.Web.Blazor.Fluxor
{
	public interface IReactiveStore
	{
		IObservable<Object> States { get; }
	}
}
