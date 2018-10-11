using Microsoft.Extensions.DependencyInjection;
using Moq;
using RichardSzalay.MockHttp;
using System;
using System.Net.Http;

namespace Lob.Net.Tests
{
    public class BaseRequestTests
    {
        public BaseRequestTests()
        {
        }

        protected IServiceCollection GetServiceProvider(Action<MockHttpMessageHandler> setupMock)
        {
            var services = new ServiceCollection();
            services.AddLob(config => { config.ApiKey = "Key"; });

            var mockHttp = new MockHttpMessageHandler();
            setupMock(mockHttp);
            var client = mockHttp.ToHttpClient();

            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            httpClientFactoryMock
                .Setup(m => m.CreateClient(It.Is<string>(n => n == "LobHttpClient")))
                .Returns(client);
            services.AddScoped(m => httpClientFactoryMock.Object);

            return services;
        }
    }
}
