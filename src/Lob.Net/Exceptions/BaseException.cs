using Lob.Net.Models;
using System;

namespace Lob.Net.Exceptions
{
    public class LobException : Exception
    {
        public ErrorResponse Error { get; }

        public LobException()
        {
        }

        public LobException(ErrorResponse error)
        {
            Error = error;
        }
    }
}
