using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerCardService.Api.Models.Output
{
    public class CardSaveOutput
    {
        public Guid CardId { get; set; }
        public TokenOutput Token { get; set; }
    }
}
