using Lob.Net.Exceptions;
using Lob.Net.Models;
using Newtonsoft.Json;
using System;
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
                /*
                var result1 = await lobPostcards.CreateAsync(new PostcardRequest
                {
                    From = new AddressRequest(new Address
                    {
                        Name = "Jean-Sébastien Goupil",
                        Company = "JSGoupil, LLC",
                        AddressLine1 = "123 Main Street",
                        AddressCity = "Seattle",
                        AddressState = "WA",
                        AddressZip = "98103",
                        AddressCountry = "US"
                    }),
                    To = new AddressRequest("adr_738379e5622a9f04"), // Saved address in LOB
                    Front = "tmpl_7e7fdb7d1cb261d", // Saved template in LOB
                    Back = "tmpl_7e7fdb7d1cb261d", // Saved template in LOB
                    MergeVariables = new Dictionary<string, string>
                    {
                        {"variable_name", "Jean-Sébastien" }
                    }
                });

                var str1 = JsonConvert.SerializeObject(result1);
                Console.WriteLine(str1);
                */
                var result2 = await lobPostcards.ListAsync(new PostcardFilter
                {
                    Limit = 1
                });
                
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
