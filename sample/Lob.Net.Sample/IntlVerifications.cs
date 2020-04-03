using Lob.Net.Exceptions;
using Lob.Net.Models;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Lob.Net.Sample
{
    class IntlVerifications
    {
        private readonly ILobIntlVerifications lobIntlVerifications;

        public IntlVerifications(
            ILobIntlVerifications lobIntlVerifications
        )
        {
            this.lobIntlVerifications = lobIntlVerifications;
        }

        public async Task Run()
        {
            try
            {
                var result1 = await lobIntlVerifications.VerifyAsync(new IntlVerificationRequest
                {
                    City = "Summerside",
                    Country = "CA",
                    PrimaryLine = "370 Water St",
                    PostalCode = "C1N 1C4"
                });

                var str1 = JsonConvert.SerializeObject(result1);
                Console.WriteLine(str1);
            }
            catch (LobException ex)
            {
                throw ex;
            }
        }
    }
}
