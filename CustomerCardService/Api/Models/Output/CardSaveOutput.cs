using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerCardService.Api.Models.Output
{
    public class CardOutput
    {
        public int CustomerId { get; set; }
        public Guid CardId { get; set; }
        public Guid Token { get; set; }
        public int CVV { get; set; }
    }
}
