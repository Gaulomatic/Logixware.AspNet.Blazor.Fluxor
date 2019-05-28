using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;

using Blazor.Fluxor;

namespace Logixware.Web.Blazor.Fluxor
{
	public class StoreInitializer : ComponentBase
	{
		/// <summary>
		/// Gets displayed when the component is pre-rendered
		/// </summary>
		[Parameter] protected RenderFragment NotConnected { get; set; }

		/// <summary>
		/// Gets displayed when the component is connected but the store is not yet initialized
		/// </summary>
		[Parameter] protected RenderFragment Connected { get; set; }

		/// <summary>
		/// Gets displayed when the store is initialized
		/// </summary>
		[Parameter] protected RenderFragment Initialized { get; set; }

		/// <summary>
		/// Gets invoked when the component is connected
		/// </summary>
		[Parameter] protected EventCallback OnConnected { get; set; }

		/// <summary>
		/// Gets invoked when the store finished initializing
		/// </summary>
		[Parameter] protected EventCallback OnInitialized { get; set; }

		[Inject] protected IStore Store { get; set; }
		[Inject] protected IComponentContext Context { get; set; }

		private Boolean _IsConnected;

		private Boolean _IsInitialized;

		protected override async Task OnAfterRenderAsync()
		{
			if (!this.Context.IsConnected)
			{
				return;
			}

			if (!this._IsConnected)
			{
				await this.OnConnectedAsync();
				this._IsConnected = true;
				await base.Invoke(base.StateHasChanged);
			}

			else if (!this._IsInitialized)
			{
//				await this.Store.Initialized;
				await this.OnInitializedAsync();
				this._IsInitialized = true;
				await base.Invoke(base.StateHasChanged);
			}
		}

		protected virtual Task OnConnectedAsync()
		{
			return this.OnConnected.HasDelegate ? this.OnConnected.InvokeAsync(null) : Task.CompletedTask;
		}

		protected virtual Task OnInitializedAsync()
		{
			return this.OnInitialized.HasDelegate ? this.OnInitialized.InvokeAsync(null) : Task.CompletedTask;
		}

		protected override void BuildRenderTree(RenderTreeBuilder builder)
		{
			base.BuildRenderTree(builder);

			if (this._IsConnected & this._IsInitialized)
			{
				builder.AddContent(0, this.Store.Initialize());
				builder.AddContent(1, this.Initialized);

				return;
			}

			if (this._IsConnected & !this._IsInitialized)
			{
				builder.AddContent(0, this.Store.Initialize());

				if (this.Connected != null)
				{
					builder.AddContent(1, this.Connected);
				}
				else if(this.NotConnected != null)
				{
					builder.AddContent(1, this.NotConnected);
				}

				return;
			}

			builder.AddContent(0, this.NotConnected);
		}
	}
}
