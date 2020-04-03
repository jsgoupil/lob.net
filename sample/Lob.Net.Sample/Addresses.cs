using Lob.Net.Exceptions;
using Lob.Net.Models;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Lob.Net.Sample
{
    class Addresses
    {
        private readonly ILobAddresses lobAddresses;

        public Addresses(
            ILobAddresses lobAddresses
        )
        {
            this.lobAddresses = lobAddresses;
        }

        public async Task Run()
        {
            try
            {
                var result1 = await lobAddresses.CreateAsync(new AddressRequest
                {
                    Name = "Jean-Sébastien Goupil",
                    Company = "JSGoupil, LLC",
                    AddressLine1 = "123 Main Street",
                    AddressCity = "Seattle",
                    AddressState = "WA",
                    AddressZip = "98103",
                    AddressCountry = "US"
                });

                var str1 = JsonConvert.SerializeObject(result1);
                Console.WriteLine(str1);

                var enumerable = lobAddresses.ListObjectsAsync();
                var enumerator = enumerable.GetAsyncEnumerator();
                await enumerator.MoveNextAsync();
                var result2 = enumerator.Current;

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
