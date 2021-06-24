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
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {

        private readonly ICardService cardService;
        public CardController(ICardService cardService)
        {
            this.cardService = cardService;
        }

        [HttpPost]
        public ActionResult<Card> PostCard(CardSaveInput card)
        {
            CardSaveOutput cardSaved = cardService.SaveCard(card);

            return CreatedAtAction(nameof(GetTokenValidity), new { cardSaved.CardId, cardSaved.Token },
                new { cardSaved.CardId, cardSaved.Token });
        }

        [HttpGet]
        public ActionResult<Card> GetTokenValidity(CardTokenValidationInput card)
        {
            bool tokenValidity = cardService.ValidateToken(card);

            return Ok(new { isTokenValid = tokenValidity });
        }
    }
}
