using System;

using Microsoft.Extensions.DependencyInjection;

using Blazor.Fluxor.DependencyInjection;

namespace Logixware.Web.Blazor.Fluxor
{
	public static class OptionsExtensions
	{
		public static Options AddReactiveMiddleware(this Options options, IServiceCollection services)
		{
			if (options == null) throw new ArgumentNullException(nameof(options));
			if (services == null) throw new ArgumentNullException(nameof(services));

			options.AddMiddleware<ReactiveMiddleware>();

			services.AddScoped<IReactiveEffects>(x => x.GetRequiredService<ReactiveMiddleware>());
			services.AddScoped<IReactiveStore>(x => x.GetRequiredService<ReactiveMiddleware>());

			return options;
		}
	}
}
