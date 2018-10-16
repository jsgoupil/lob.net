using Lob.Net.Models;
using System.Threading.Tasks;

namespace Lob.Net
{
    internal class LobIntlVerifications : ILobIntlVerifications
    {
        private const string URL_VERIFICATIONS = "/v1/intl_verifications";
        protected ILobCommunicator lobCommunicator;

        public LobIntlVerifications(
            ILobCommunicator lobCommunicator
        )
        {
            this.lobCommunicator = lobCommunicator;
        }

        public Task<IntlVerificationResponse> Verify(IntlVerificationRequest request)
        {
            return lobCommunicator.PostAsync<IntlVerificationResponse>(URL_VERIFICATIONS, request);
        }
    }
}
