using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System;

namespace Lob.Net.Formatters
{
    public static class MvcBuilderExtensions
    {
        public static IMvcBuilder AddLobFormatters(this IMvcBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            AddLobFormatterServices(builder.Services);
            return builder;
        }

        internal static void AddLobFormatterServices(IServiceCollection services)
        {
            services.TryAddEnumerable(
                ServiceDescriptor.Transient<IConfigureOptions<MvcJsonOptions>, LobSerializerMvcOptionsSetup>());
        }
    }
}
