using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerCardService.Api.Models.Output
{
    public class CardSaveOutput
    {
        public DateTimeOffset TokenCreationDate { get; set; }
        public Guid Token { get; set; }
        public Guid CardId { get; set; }


    }
}
