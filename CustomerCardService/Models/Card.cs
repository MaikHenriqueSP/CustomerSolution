using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerCardService.Models
{
    public class Card
    {
        private Guid Token { get; set; }
        private Guid CardId { get; }
        private int CustomerId { get; }
        public long CardNumber { get; }
        public int CVV { get; }

        public Card(int customerId, long cardNumber, int cVV)
        {
            CustomerId = customerId;
            CardNumber = cardNumber;
            CVV = cVV;
        }
    }
}
