using CustomerCardService.Core.Utilities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerCardService.Domain.Models
{
    public class Card
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid CardId { get; set; }

        [Required]
        public Customer Customer { get; set; }

        [Required]
        private long _cardNumber;

        [Required]
        private int _cVV;

        public Token Token { get; set; }

        public int CVV
        {
            get => _cVV;
            set
            {
                if (!ValidationUtilities.IsNumberWithinMaxNumberOfDigits(value, 5))
                {
                    throw new ArgumentException("The CVV shouldn't have more than 5 digits");
                }

                if (value < 0)
                {
                    throw new ArgumentException("The CVV shouldn't be negative");
                }

                _cVV = value;
            }
        }

        public long CardNumber
        {
            get => _cardNumber;
            set
            {
                if (!ValidationUtilities.IsNumberWithinMaxNumberOfDigits(value, 16))
                {
                    throw new ArgumentException("The Card Number shouldn't have more than 16 digits");
                }

                if (value < 0)
                {
                    throw new ArgumentException("The CVV shouldn't be negative");
                }

                _cardNumber = value;
            }
        }


    }
}
