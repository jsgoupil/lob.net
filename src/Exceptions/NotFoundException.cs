using Lob.Net.Models;

namespace Lob.Net.Exceptions
{
    public class NotFoundException : LobException
    {
        public NotFoundException(ErrorResponse error)
            : base(error)
        {
        }
    }
}
