using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerCardService.Domain.Exceptions
{
    public class TokenExpiredException : Exception
    {
        private static readonly string defaultMessage = "The provided token has expired";

        public TokenExpiredException(): base(defaultMessage)
        {
        }

        public TokenExpiredException(string message)
            : base(message)
        {
        }

        public TokenExpiredException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
