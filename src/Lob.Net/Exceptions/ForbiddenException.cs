using Lob.Net.Models;

namespace Lob.Net.Exceptions
{
    public class ForbiddenException : LobException
    {
        public ForbiddenException(ErrorResponse error)
            : base(error)
        {
        }
    }
}
