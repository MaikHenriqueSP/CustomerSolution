using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerCardService.Core.Utilities
{
    /// <summary>
    /// Provides generic validations implementations as a library.
    /// </summary>
    public static class ValidationUtilities
    {
        public static bool IsNumberWithinMaxNumberOfDigits(long number, int numberOfDigits) 
        {
            int numberLength = (int)Math.Log10(Math.Abs(number)) + 1;
            return numberLength <= numberOfDigits;
        }

        public static bool IsNumberWithinMinNumberOfDigits(long number, int numberOfDigits) 
        {
            int numberLength = (int)Math.Log10(Math.Abs(number)) + 1;
            return numberLength >= numberOfDigits;
        }
    }
}
