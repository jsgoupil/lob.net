using Lob.Net.Models;
using System.Threading.Tasks;

namespace Lob.Net
{
    public interface ILobUsVerifications
    {
        Task<UsVerificationResponse> Verify(UsVerificationRequest request, UsVerificationCase @case = UsVerificationCase.Upper);
        Task<UsAutocompletionResponse> Autocomplete(UsAutocompletionRequest request, string ipAddress = null);
        Task<UsZipLookupResponse> ZipLookup(string zipCode);
        Task<UsZipLookupResponse> ZipLookup(UsZipLookupRequest request);
    }
}
