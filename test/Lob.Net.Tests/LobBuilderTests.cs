using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using Xunit;

namespace Lob.Net.Tests
{
    public class LobBuilderTests
    {
        [Fact]
        public void ServicesAdded()
        {
            var key = "ABC";

            var services = new ServiceCollection();
            services.AddLob(config => { config.ApiKey = key; });
            var sp = services.BuildServiceProvider();

            Assert.NotNull(sp.GetService<ILobLetters>());
            Assert.NotNull(sp.GetService<ILobPostcards>());
            Assert.NotNull(sp.GetService<ILobChecks>());
            Assert.NotNull(sp.GetService<ILobBankAccounts>());
        }

        [Fact]
        public void SettingsWork()
        {
            var key = "ABC";

            var services = new ServiceCollection();
            services.AddLob(config => { config.ApiKey = key; });
            var sp = services.BuildServiceProvider();

            var options = sp.GetService<IOptions<LobOptions>>();
            Assert.NotNull(options);
            Assert.Equal(key, options.Value.ApiKey);
        }

        [Fact]
        public void MissingKey()
        {
            var services = new ServiceCollection();
            services.AddLob(config => {  });
            var sp = services.BuildServiceProvider();

            Assert.ThrowsAny<Exception>(() => sp.GetService<ILobLetters>());
        }
    }
}
