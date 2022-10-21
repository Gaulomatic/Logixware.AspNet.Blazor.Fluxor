using Fluxor.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Logixware.AspNet.Blazor.Fluxor
{
	/// <summary>
	/// Fluxor options extensions for adding stuff to Fluxor
	/// </summary>
	public static class OptionsExtensions
	{
        /// <summary>
        /// Adds the Reactive middleware and <see cref="T:Logixware.AspNet.Blazor.Fluxor.IObservableStore" /> service
        /// </summary>
        /// <param name="options">Teh Fluxor options</param>
        /// <param name="services">The <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" /></param>
        /// <returns>The <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" /></returns>
        /// <exception cref="ArgumentNullException">If options is null</exception>
        /// <exception cref="ArgumentNullException">If services is null</exception>
        public static FluxorOptions AddReactiveStore(this FluxorOptions options, IServiceCollection services)
		{
			if (options == null) throw new ArgumentNullException(nameof(options));
			if (services == null) throw new ArgumentNullException(nameof(services));

			options.AddMiddleware<ObservableMiddleware>();
			services.AddScoped<IObservableStore>(x => x.GetRequiredService<ObservableMiddleware>());

			return options;
		}
	}
}
