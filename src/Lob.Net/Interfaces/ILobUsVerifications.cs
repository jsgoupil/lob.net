using Lob.Net.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Lob.Net
{
    public interface ILobUsVerifications
    {
        Task<UsVerificationResponse> VerifyAsync(UsVerificationRequest request, UsVerificationCase @case = UsVerificationCase.Upper, CancellationToken cancellationToken = default);
        Task<UsAutocompletionResponse> AutocompleteAsync(UsAutocompletionRequest request, string ipAddress = default, CancellationToken cancellationToken = default);
        Task<UsZipLookupResponse> ZipLookupAsync(string zipCode, CancellationToken cancellationToken = default);
        Task<UsZipLookupResponse> ZipLookupAsync(UsZipLookupRequest request, CancellationToken cancellationToken = default);
    }
}
