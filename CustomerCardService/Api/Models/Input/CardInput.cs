using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerCardService.Api.Models.Input
{
    public class CardInput
    {
        public int CustomerId { get; set; }
        public long CardNumber { get; set; }
        public int CVV { get; set; }
    }
}
