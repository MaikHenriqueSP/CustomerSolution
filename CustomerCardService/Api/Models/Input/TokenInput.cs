using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerCardService.Api.Models.Input
{
    public class TokenInput
    {
        [Required]
        public Guid TokenValue { get; set; }
    }
}
