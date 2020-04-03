using Lob.Net.Models;

namespace Lob.Net
{
    public class LobPostcards : LobBaseRequest<PostcardRequest, PostcardResponse, PostcardFilter>, ILobPostcards
    {
        private const string URL = "/v1/postcards";

        public LobPostcards(
            ILobCommunicator lobCommunicator
        )
            : base(lobCommunicator, URL)
        {
        }
    }
}
