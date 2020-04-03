using Lob.Net.Exceptions;
using Lob.Net.Models;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Lob.Net.Sample
{
    class BankAccounts
    {
        private readonly ILobBankAccounts lobBankAccounts;

        public BankAccounts(
            ILobBankAccounts lobBankAccounts
        )
        {
            this.lobBankAccounts = lobBankAccounts;
        }

        public async Task Run()
        {
            try
            {
                var result1 = await lobBankAccounts.CreateAsync(new BankAccountRequest
                {
                    AccountNumber = "123456789",
                    RoutingNumber = "322271627",
                    AccountType = AccountType.Individual,
                    Description = "My Personal Account",
                    Signatory = "Jean-Sébastien Goupil"
                });

                var str1 = JsonConvert.SerializeObject(result1);
                Console.WriteLine(str1);

                var enumerable = lobBankAccounts.ListObjectsAsync();
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
