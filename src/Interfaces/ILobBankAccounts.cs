using Lob.Net.Models;
using System.Threading.Tasks;

namespace Lob.Net
{
    public interface ILobBankAccounts
    {
        Task<BankAccountResponse> CreateAsync(BankAccountRequest bankAccount, string idempotencyKey = null);
        Task<BankAccountResponse> RetrieveAsync(string id);
        Task<DeleteResponse> DeleteAsync(string id);
        Task<BankAccountResponse> VerifyAsync(string id, int amountInCents1, int amountInCents2);
        Task<ListResponse<BankAccountResponse>> ListAsync(BankAccountFilter filter = null);
    }
}
