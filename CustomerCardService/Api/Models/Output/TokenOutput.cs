using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerCardService.Api.Models.Output
{
    public class TokenOutput
    {
        public DateTimeOffset CreationDate { get; set; }
        public Guid TokenValue { get; set; }
    }
}
