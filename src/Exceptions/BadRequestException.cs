using Lob.Net.Models;

namespace Lob.Net.Exceptions
{
    public class BadRequestException : LobException
    {
        public BadRequestException(ErrorResponse error)
            : base(error)
        {
        }
    }
}
