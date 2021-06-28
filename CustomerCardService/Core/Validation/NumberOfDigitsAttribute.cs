using System.ComponentModel.DataAnnotations;

namespace CustomerCardService.Core.Validation
{
    /// <summary>
    /// Custom Data Annotation that provides validation for the number of digits of a number.
    /// It checks if the given input is within the specified range of digits.
    /// </summary>
    public class NumberOfDigitsAttribute : ValidationAttribute
    {
        private readonly int minLength;
        private readonly int maxLength;
        private static readonly string defaultMessage = "The number of digits is not within the allowed range";

        public NumberOfDigitsAttribute(int maxLength, int minLength = 0) : base(defaultMessage)
        {
            this.minLength = minLength;
            this.maxLength = maxLength;
        }

        public override bool IsValid(object value)
            
        {
            if (value == null)
            {
                return true;
            }

            string valueParsed = value.ToString();
            bool validParsing = long.TryParse(valueParsed, out long result);

            if (!validParsing)
            {
                return false;
            }
            int numberLength = valueParsed.Length;

            return numberLength >= minLength && numberLength <= maxLength;
        }

    }
}
