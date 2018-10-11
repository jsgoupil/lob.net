using Lob.Net.Models;

namespace Lob.Net.Exceptions
{
    public class ServerErrorException : LobException
    {
        public ServerErrorException(ErrorResponse error)
            : base(error)
        {
        }
    }
}
