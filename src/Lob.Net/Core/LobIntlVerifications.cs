using Lob.Net.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Lob.Net
{
    public class LobIntlVerifications : ILobIntlVerifications
    {
        private const string URL_VERIFICATIONS = "/v1/intl_verifications";
        protected ILobCommunicator lobCommunicator;

        public LobIntlVerifications(
            ILobCommunicator lobCommunicator
        )
        {
            this.lobCommunicator = lobCommunicator;
        }

        public Task<IntlVerificationResponse> VerifyAsync(IntlVerificationRequest request, CancellationToken cancellationToken = default)
        {
            return lobCommunicator.PostAsync<IntlVerificationResponse>(URL_VERIFICATIONS, request, cancellationToken);
        }
    }
}
