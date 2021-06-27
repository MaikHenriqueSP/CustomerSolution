using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerCardService.Domain.Models
{
    public class Token
    {
        [Key]
        public Guid TokenValue { get; set; }

        public DateTimeOffset CreationDate { get; set; }
    }
}
