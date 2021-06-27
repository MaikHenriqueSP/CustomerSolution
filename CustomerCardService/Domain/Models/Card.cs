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
        public Guid CardId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public long CardNumber { get; set; }
        
        [Required]
        public int CVV { get; set; }

        public Guid Token { get; set; }

        public DateTimeOffset TokenCreationDate { get; set; }

    }
}
