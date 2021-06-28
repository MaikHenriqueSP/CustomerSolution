using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerCardService.Domain.Exceptions
{
    /// <summary>
    /// Exception for data inconsistency between to cards compared agaisn't each other.
    /// </summary>
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
