using AutoMapper;
using CustomerCardService.Api.Models.Input;
using CustomerCardService.Api.Models.Output;
using CustomerCardService.Domain.Models;
using CustomerCardService.Domain.Repository;
using CustomerCardService.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerCardService.Api.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {

        private readonly ICardService cardService;
        private readonly IMapper mapper;
        public CardController(ICardService cardService, IMapper mapper)
        {
            this.cardService = cardService;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<Card>> PostCard(CardSaveInput card)
        {
            Card cardMapped = mapper.Map<Card>(card);
            Card cardSaved = await cardService.SaveCard(cardMapped);

            CardSaveOutput cardSavedOutput = mapper.Map<CardSaveOutput>(cardSaved);

            return CreatedAtAction(nameof(GetTokenValidity), cardSavedOutput);
        }

        [Route("token/validity")]
        [HttpPost]
        public async Task<ActionResult<Card>> GetTokenValidity(CardTokenValidationInput card)
        {
            Card cardMapped = mapper.Map<Card>(card);
            bool tokenValidity = await cardService.ValidateToken(cardMapped);

            return Ok(new { isTokenValid = tokenValidity });
        }
    }
}
