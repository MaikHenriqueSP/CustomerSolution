using AutoMapper;
using CustomerCardService.Api.Models.Input;
using CustomerCardService.Api.Models.Output;
using CustomerCardService.Domain.Models;
using CustomerCardService.Domain.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerCardService.Domain.Services
{
    public class CardService : ICardService
    {
        private readonly CardContext cardContext;
        private readonly IMapper mapper;
        public CardService(CardContext cardContext, IMapper mapper)
        {
            this.cardContext = cardContext;
            this.mapper = mapper;
        }

        public Guid GenerateToken(CardSaveInput card)
        {
            throw new NotImplementedException();
        }

        public CardSaveOutput SaveCard(CardSaveInput cardInput)
        {
            Card cardOrDefault = cardContext.Cards
                .SingleOrDefault(c => c.CardNumber == cardInput.CardNumber);

            if (cardOrDefault == null)
            {
                //Guid token = GenerateToken(cardInput);
                Card card = mapper.Map<Card>(cardInput);
                cardContext.AddAsync(card);
                cardContext.SaveChangesAsync();
                return mapper.Map<CardSaveOutput>(card);
            }

            //@TODO: Validate the card input against the one found
            cardOrDefault.Token = System.Guid.NewGuid();
            cardContext.AddAsync(cardOrDefault);
            cardContext.SaveChangesAsync();

            return mapper.Map<CardSaveOutput>(cardOrDefault);

        }

        public bool ValidateToken(CardTokenValidationInput card)
        {
            throw new NotImplementedException();
        }
    }
}
