using CustomerCardService.Domain.Validation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

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
        [NumberOfDigits(maxLength: 16,
            ErrorMessage = "The CardNumber field shouldn't have more than 16 digits.")]        
        public long CardNumber { get; set; }
        [Required]
        [NumberOfDigits(maxLength: 5,
            ErrorMessage = "The CVV field shouldn't have more than 5 digits.")]
        public int CVV { get; set; }

        public Guid Token { get; set; }
        
        public DateTimeOffset TokenCreationDate { get; set; }

    }
}
