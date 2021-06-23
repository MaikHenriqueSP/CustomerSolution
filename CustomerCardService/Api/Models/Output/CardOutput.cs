using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerCardService.Api.Models.Output
{
    public class CardOutput
    {
        public int CustomerId { get; }
        public Guid CardId { get; }
        public Guid Token { get; }
        public int CVV { get; }
    }
}
