using Lob.Net.Models;

namespace Lob.Net
{
    public class LobChecks : LobBaseRequest<CheckRequest, CheckResponse, CheckFilter>, ILobChecks
    {
        private const string URL = "/v1/checks";

        public LobChecks(
            ILobCommunicator lobCommunicator
        )
            : base(lobCommunicator, URL)
        {
        }
    }
}
