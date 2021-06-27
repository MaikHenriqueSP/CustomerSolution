using CustomerCardService.Core.Validation;
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
        public Guid CardId { get; private set; }

        [Required]
        public int CustomerId { get; private set; }

        [Required]
        public long CardNumber { get; private set; }
        
        [Required]
        public int CVV { get; private set; }

        public Guid Token { get; set; }

        public DateTimeOffset TokenCreationDate { get; set; }

        public Card(int customerId, long cardNumber, int cVV)
        {
            CustomerId = customerId;
            CardNumber = cardNumber;
            CVV = cVV;
        }
    }
}
