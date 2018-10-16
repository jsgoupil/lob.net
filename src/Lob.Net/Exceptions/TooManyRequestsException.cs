using Lob.Net.Models;

namespace Lob.Net.Exceptions
{
    public class TooManyRequestsException : LobException
    {
        public TooManyRequestsException(ErrorResponse error)
            : base(error)
        {
        }
    }
}
