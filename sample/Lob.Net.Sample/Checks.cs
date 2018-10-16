using Lob.Net.Exceptions;
using Lob.Net.Models;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Lob.Net.Sample
{
    class Checks
    {
        private readonly ILobChecks lobChecks;

        public Checks(
            ILobChecks lobChecks
        )
        {
            this.lobChecks = lobChecks;
        }

        public async Task Run()
        {
            try
            {


                var result1 = await lobChecks.CreateAsync(new CheckRequest
                {
                    Amount = 100,
                    BankAccount = "bank_be392350a667a24", // Saved bank account in LOB
                    From = new AddressReference(new AddressRequest
                    {
                        Name = "Jean-Sébastien Goupil",
                        Company = "JSGoupil, LLC",
                        AddressLine1 = "123 Main Street",
                        AddressCity = "Seattle",
                        AddressState = "WA",
                        AddressZip = "98103",
                        AddressCountry = "US"
                    }),
                    To = new AddressReference("adr_738379e5622a9f04"), // Saved address in LOB
                    Description = "Paying Employee"
                });

                var str1 = JsonConvert.SerializeObject(result1);
                Console.WriteLine(str1);

                var result2 = await lobChecks.ListAsync();

                var str2 = JsonConvert.SerializeObject(result2);
                Console.WriteLine(str2);
            }
            catch (LobException ex)
            {
                throw ex;
            }
        }
    }
}
