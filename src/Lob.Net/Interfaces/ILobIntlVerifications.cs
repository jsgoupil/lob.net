using Lob.Net.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Lob.Net
{
    public interface ILobIntlVerifications
    {
        Task<IntlVerificationResponse> VerifyAsync(IntlVerificationRequest request, CancellationToken cancellationToken = default);
    }
}
