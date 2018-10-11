using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Threading.Tasks;

namespace Lob.Net.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location))
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfiguration configuration = builder.Build();

            var services = new ServiceCollection()
                .AddLob(options => options.ApiKey = configuration["Lob:ApiKey"]);

            services.AddScoped((_) => configuration);

            var container = services.BuildServiceProvider();

            var letters = new Letters(container.GetService<ILobLetters>());
            var postcards = new Postcards(container.GetService<ILobPostcards>());
            var bankAccounts = new BankAccounts(container.GetService<ILobBankAccounts>());
            var checks = new Checks(container.GetService<ILobChecks>());

            Task.Run(async () =>
            {
                await letters.Run();
                await postcards.Run();
                await bankAccounts.Run();
                await checks.Run();
            }).GetAwaiter().GetResult();
        }
    }
}