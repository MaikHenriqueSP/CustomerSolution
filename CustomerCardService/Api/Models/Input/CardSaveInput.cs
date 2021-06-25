using CustomerCardService.Core.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerCardService.Api.Models.Input
{
    public class CardSaveInput
    {
        [Required]
        public int CustomerId { get; set; }

        [Required]
        [NumberOfDigits(maxLength: 16,
            ErrorMessage = "The CardNumber field shouldn't have more than 16 digits.")]

        public long CardNumber { get; set; }
        [Required]
        [NumberOfDigits(maxLength: 5,
            ErrorMessage = "The CVV field shouldn't have more than 5 digits.")]
        public int CVV { get; set; }
    }
}
