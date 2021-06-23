using CustomerCardService.Models;
using CustomerCardService.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerCardService.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly CardContext _cardContext;

        public CardController(CardContext cardContext)
        {
            _cardContext = cardContext;
        }

        [HttpPost]
        public async Task<ActionResult<Card>> PostCard(Card card)
        {
            _cardContext.Cards.Add(card);
            await _cardContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSomething), new { card.CardId, card.Token },
                new { card.CardId, card.Token });
        }

        [HttpGet("{cardId}")]
        public async Task<ActionResult<Card>> GetSomething(Guid cardId)
        {
            var card = await _cardContext.Cards.FindAsync(cardId);

            if (card == null)
            {
                return NotFound();
            }

            return card;
        }
    }
}
