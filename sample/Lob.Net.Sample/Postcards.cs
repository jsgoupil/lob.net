using Lob.Net.Exceptions;
using Lob.Net.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lob.Net.Sample
{
    class Postcards
    {
        private readonly ILobPostcards lobPostcards;

        public Postcards(
            ILobPostcards lobPostcards
        )
        {
            this.lobPostcards = lobPostcards;
        }

        public async Task Run()
        {
            try
            {
                var result1 = await lobPostcards.CreateAsync(new PostcardRequest
                {
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
                    To = new AddressReference("adr_9162050afe1ffe96"), // Saved address in LOB
                    Front = "tmpl_c65f6b82cddb8ab", // Saved template in LOB
                    Back = "tmpl_c65f6b82cddb8ab", // Saved template in LOB
                    MergeVariables = new Dictionary<string, string>
                    {
                        {"variable_name", "Jean-Sébastien" }
                    }
                });

                var str1 = JsonConvert.SerializeObject(result1);
                Console.WriteLine(str1);

                var enumerable = lobPostcards.ListObjectsAsync(new PostcardFilter
                {
                    Limit = 1
                });
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
