using CustomerCardService.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerCardService.Api.Models.Input
{
    public class CardTokenValidationInput
    {
        [Required]
        public CustomerInput Customer { get; set; }

        [Required]
        public Guid CardId { get; set; }
        
        [Required]
        public TokenInput TokenInput { get; set; }
        
        [Required]
        public int CVV { get; set; }
    }
}
