using Fluxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Logixware.AspNet.Blazor.Fluxor
{
	/// <summary>
	/// Implements members to make the store observable.
	/// </summary>
	public class ObservableMiddleware : Middleware, IObservableStore
	{
		private readonly List<IObserver<object>> _ActionSubscribers;
		private readonly List<IObserver<object>> _StateSubscribers;
		private readonly Dictionary<Type, object> _States;
		private IStore _Store;

		public ObservableMiddleware()
		{
			this._ActionSubscribers = new List<IObserver<object>>();
			this._StateSubscribers = new List<IObserver<object>>();
			this._States = new Dictionary<Type, object>();
		}

		/// <summary>
		/// Returns an <see cref="T:System.IObservable`1" /> for the state objects inside the store.
		/// </summary>
		public IObservable<object> States
		{
			get
			{
				return Observable.Create<object>(o =>
				{
					this._StateSubscribers.Add(o);
					return () => this._StateSubscribers.Remove(o);
				});
			}
		}

		/// <summary>
		/// Returns an <see cref="T:System.IObservable`1" /> for actions which are dispatched.
		/// </summary>
		public IObservable<object> Actions
		{
			get
			{
				return Observable.Create<object>(o =>
				{
					this._ActionSubscribers.Add(o);
					return () => this._ActionSubscribers.Remove(o);
				});
			}
		}

		public override Task InitializeAsync(IDispatcher dispatcher, IStore store)
		{
			base.InitializeAsync(dispatcher, store);

			_Store = store;

			return Task.CompletedTask;
		}

		public override void BeforeDispatch(object action)
		{
			base.BeforeDispatch(action);

			foreach (var __Feature in _Store.Features.Values.OrderBy(x => x.GetName()))
			{
				this._States.Add(__Feature.GetStateType(), __Feature.GetState());
			}
		}

		public override void AfterDispatch(object action)
		{
			base.AfterDispatch(action);

			this._ActionSubscribers.ForEach(x => x.OnNext(action));

			foreach (var __Feature in _Store.Features.Values.OrderBy(x => x.GetName()))
			{
				if (this._States.ContainsKey(__Feature.GetStateType()) && !object.ReferenceEquals(__Feature.GetState(), this._States[__Feature.GetStateType()]))
				{
					this._StateSubscribers.ForEach(x => x.OnNext(__Feature.GetState()));
				}
			}

			this._States.Clear();
		}
	}
}
