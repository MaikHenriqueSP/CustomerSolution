using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerCardService.Models
{
    public class Card
    {
        public Guid Token { get; set; }
        [Key]
        public Guid CardId { get; set; }
        public int CustomerId { get; set; }
        public long CardNumber { get; set; }
        public int CVV { get; set; }


    }
}
