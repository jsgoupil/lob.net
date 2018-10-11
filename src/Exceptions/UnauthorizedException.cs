using Lob.Net.Models;

namespace Lob.Net.Exceptions
{
    public class UnauthorizedException : LobException
    {
        public UnauthorizedException(ErrorResponse error)
            : base(error)
        {
        }
    }
}
