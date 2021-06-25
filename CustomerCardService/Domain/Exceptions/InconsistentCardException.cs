using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerCardService.Domain.Exceptions
{
    public class InconsistentCardException : Exception
    {
        private static readonly string defaultMessage = "The provided data is inconsistent for the given card";

        public InconsistentCardException() : base(defaultMessage)
        {
        }

        public InconsistentCardException(string message)
            : base(message)
        {
        }

        public InconsistentCardException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
