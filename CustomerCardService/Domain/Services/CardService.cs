using CustomerCardService.Api.Models.Input;
using CustomerCardService.Api.Models.Output;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerCardService.Domain.Services
{
    public class CardService : ICardService
    {
        public Guid GenerateToken(CardSaveInput card)
        {
            throw new NotImplementedException();
        }

        public CardSaveOutput SaveCard(CardSaveInput card)
        {
            throw new NotImplementedException();
        }

        public bool ValidateToken(CardTokenValidationInput card)
        {
            throw new NotImplementedException();
        }
    }
}
