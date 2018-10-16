using Microsoft.Extensions.DependencyInjection;

namespace Lob.Net.Formatters.Tests
{
    public class BaseTests
    {
        public BaseTests()
        {
        }

        protected IServiceCollection GetServiceProvider()
        {
            var services = new ServiceCollection();
            services.AddLob(config => { config.ApiKey = "Key"; });

            return services;
        }
    }
}
