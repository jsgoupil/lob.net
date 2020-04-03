using Lob.Net.Models;

namespace Lob.Net
{
    public class LobLetters : LobBaseRequest<LetterRequest, LetterResponse, LetterFilter>, ILobLetters
    {
        private const string URL = "/v1/letters";

        public LobLetters(
            ILobCommunicator lobCommunicator
        )
            : base(lobCommunicator, URL)
        {
        }
    }
}
