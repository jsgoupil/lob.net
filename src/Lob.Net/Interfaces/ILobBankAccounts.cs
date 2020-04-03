using Lob.Net.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Lob.Net
{
    public interface ILobBankAccounts
    {
        Task<BankAccountResponse> CreateAsync(BankAccountRequest bankAccount, string idempotencyKey = default, CancellationToken cancellationToken = default);
        Task<BankAccountResponse> RetrieveAsync(string id, CancellationToken cancellationToken = default);
        Task<DeleteResponse> DeleteAsync(string id, CancellationToken cancellationToken = default);
        Task<BankAccountResponse> VerifyAsync(string id, int amountInCents1, int amountInCents2, CancellationToken cancellationToken = default);
        Task<ListResponse<BankAccountResponse>> ListAsync(BankAccountFilter filter = default, CancellationToken cancellationToken = default);
#if NETSTANDARD2_1
        IAsyncEnumerable<BankAccountResponse> ListObjectsAsync(BankAccountFilter filter = default, CancellationToken cancellationToken = default);
#endif
    }
}
