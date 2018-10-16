using Lob.Net.Exceptions;
using Lob.Net.Models;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Lob.Net.Sample
{
    class UsVerifications
    {
        private readonly ILobUsVerifications lobUsVerifications;

        public UsVerifications(
            ILobUsVerifications lobUsVerifications
        )
        {
            this.lobUsVerifications = lobUsVerifications;
        }

        public async Task Run()
        {
            try
            {
                var result1 = await lobUsVerifications.Verify(new UsVerificationRequest("residential house"));

                var str1 = JsonConvert.SerializeObject(result1);
                Console.WriteLine(str1);

                var result2 = await lobUsVerifications.Autocomplete(new UsAutocompletionRequest
                {
                    AddressPrefix = "5 su"
                });

                var str2 = JsonConvert.SerializeObject(result2);
                Console.WriteLine(str2);

                var result3 = await lobUsVerifications.ZipLookup(new UsZipLookupRequest
                {
                    ZipCode = "94133"
                });

                var str3 = JsonConvert.SerializeObject(result3);
                Console.WriteLine(str3);
            }
            catch (LobException ex)
            {
                throw ex;
            }
        }
    }
}
