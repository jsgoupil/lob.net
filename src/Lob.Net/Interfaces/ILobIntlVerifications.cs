using Lob.Net.Models;
using System.Threading.Tasks;

namespace Lob.Net
{
    public interface ILobIntlVerifications
    {
        Task<IntlVerificationResponse> Verify(IntlVerificationRequest request);
    }
}
