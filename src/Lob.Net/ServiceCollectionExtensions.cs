using Microsoft.Extensions.DependencyInjection;
using System;

namespace Lob.Net
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddLob(this IServiceCollection services,
            Action<LobOptions> setupAction
        )
        {
            services
                .AddSingleton<ILobCommunicator, LobCommunicator>()
                .AddSingleton<ILobAddresses, LobAddresses>()
                .AddSingleton<ILobBankAccounts, LobBankAccounts>()
                .AddSingleton<ILobChecks, LobChecks>()
                .AddSingleton<ILobLetters, LobLetters>()
                .AddSingleton<ILobPostcards, LobPostcards>()
                .AddSingleton<ILobUsVerifications, LobUsVerifications>()
                .AddSingleton<ILobIntlVerifications, LobIntlVerifications>()
                .AddSingleton<LobSerializerSettings>()
                .AddHttpClient(CoreValues.HTTP_CLIENT_NAME);

            if (setupAction != null)
            {
                services.ConfigureLob(setupAction);
            }

            return services;
        }

        public static IServiceCollection ConfigureLob(this IServiceCollection services, Action<LobOptions> configure)
            => services.Configure(configure);
    }
}
