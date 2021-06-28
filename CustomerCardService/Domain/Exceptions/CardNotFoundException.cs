using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerCardService.Domain.Exceptions
{
    public class CardNotFoundException : Exception
    {
        private static readonly string defaultMessage = "There is no card with the provided information";

        public CardNotFoundException() : base(defaultMessage)
        {
        }

        public CardNotFoundException(string message)
            : base(message)
        {
        }

        public CardNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
