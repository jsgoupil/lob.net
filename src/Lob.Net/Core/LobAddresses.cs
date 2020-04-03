using Lob.Net.Models;

namespace Lob.Net
{
    public class LobAddresses : LobBaseRequest<AddressRequest, AddressResponse, AddressFilter>, ILobAddresses
    {
        private const string URL = "/v1/addresses";

        public LobAddresses(
            ILobCommunicator lobCommunicator
        )
            : base(lobCommunicator, URL)
        {
        }
    }
}
