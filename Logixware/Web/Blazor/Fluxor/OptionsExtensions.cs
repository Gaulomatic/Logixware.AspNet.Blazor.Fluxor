using System;

using Microsoft.Extensions.DependencyInjection;

using Blazor.Fluxor.DependencyInjection;

namespace Logixware.Web.Blazor.Fluxor
{
	/// <summary>
	/// Fluxor options extensions for adding stuff to Fluxor
	/// </summary>
	public static class OptionsExtensions
	{
		/// <summary>
		/// Adds the Reactive middleware and <see cref="T:Logixware.Web.Blazor.Fluxor.IReactiveStore" /> service
		/// </summary>
		/// <param name="options">Teh Fluxor options</param>
		/// <param name="services">The <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" /></param>
		/// <returns>The <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" /></returns>
		/// <exception cref="ArgumentNullException">If options is null</exception>
		/// <exception cref="ArgumentNullException">If services is null</exception>
		public static Options AddReactiveStore(this Options options, IServiceCollection services)
		{
			if (options == null) throw new ArgumentNullException(nameof(options));
			if (services == null) throw new ArgumentNullException(nameof(services));

			options.AddMiddleware<ReactiveMiddleware>();
			services.AddScoped<IReactiveStore>(x => x.GetRequiredService<ReactiveMiddleware>());

			return options;
		}
	}
}
