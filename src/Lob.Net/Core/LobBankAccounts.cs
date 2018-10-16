using Lob.Net.Models;
using System.Threading.Tasks;

namespace Lob.Net
{
    internal class LobBankAccounts : LobBaseRequest<BankAccountRequest, BankAccountResponse, BankAccountFilter>, ILobBankAccounts
    {
        private const string URL = "/v1/bank_accounts";

        public LobBankAccounts(
            ILobCommunicator lobCommunicator
        )
            : base (lobCommunicator, URL)
        {
        }

        public Task<BankAccountResponse> VerifyAsync(string id, int amountInCents1, int amountInCents2)
        {
            return lobCommunicator.PostAsync<BankAccountResponse>($"{URL}/{id}/verify", new
            {
                Amounts = new int[]
                {
                    amountInCents1,
                    amountInCents2
                }
            });
        }
    }
}
