using System.ComponentModel.DataAnnotations;

namespace CustomerCardService.Core.Validation
{
    public class NumberOfDigitsAttribute : ValidationAttribute
    {
        private readonly int minLength;
        private readonly int maxLength;
        
        public NumberOfDigitsAttribute(int maxLength, int minLength = 0) : base("")
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
