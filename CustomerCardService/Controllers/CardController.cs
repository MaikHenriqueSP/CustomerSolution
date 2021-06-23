using CustomerCardService.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerCardService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly CardContext _cardContext;

        public CardController(CardContext cardContext)
        {
            _cardContext = cardContext;
        }


    }
}
