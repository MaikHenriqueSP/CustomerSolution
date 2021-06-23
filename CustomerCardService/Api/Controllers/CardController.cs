using AutoMapper;
using CustomerCardService.Api.Models.Input;
using CustomerCardService.Api.Models.Output;
using CustomerCardService.Domain.Models;
using CustomerCardService.Domain.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerCardService.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly CardContext _cardContext;
        public readonly IMapper _mapper;


        public CardController(CardContext cardContext, IMapper iMapper)
        {
            _cardContext = cardContext;
            _mapper = iMapper;
        }

        [HttpPost]
        public async Task<ActionResult<Card>> PostCard(CardInput cardInput)
        {
            Card cardSaved = _mapper.Map<Card>(cardInput);
            _cardContext.Cards.Add(cardSaved);
            await _cardContext.SaveChangesAsync();

            CardOutput card = _mapper.Map<CardOutput>(cardSaved);

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
