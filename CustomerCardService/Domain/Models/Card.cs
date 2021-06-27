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
        public int CustomerId { get; set; }

        [Required]
        public long CardNumber { get; set; }

        [Required]
        public int CVV { get; set; }

        public Guid Token { get; set; }

        public DateTimeOffset TokenCreationDate { get; set; }

        public void SetCVV(int cVV)
        {
            if (!ValidationUtilities.IsNumberWithinMaxNumberOfDigits(cVV, 5))
            {
                throw new ArgumentException("The CVV shouldn't have more than 5 digits");
            }

            if (cVV < 0)
            {
                throw new ArgumentException("The CVV shouldn't be negative");
            }

            CVV = cVV;
        }

        public void SetCardNumber(int cardNumber)
        {
            if (!ValidationUtilities.IsNumberWithinMaxNumberOfDigits(cardNumber, 16))
            {
                throw new ArgumentException("The Card Number shouldn't have more than 16 digits");
            }

            if (cardNumber < 0)
            {
                throw new ArgumentException("The CVV shouldn't be negative");
            }

            CardNumber = cardNumber;
        }


    }
}
