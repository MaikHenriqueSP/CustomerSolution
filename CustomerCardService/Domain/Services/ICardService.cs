using CustomerCardService.Api.Models.Input;
using CustomerCardService.Api.Models.Output;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerCardService.Domain.Services
{
    public interface ICardService
    {
        CardSaveOutput SaveCard(CardSaveInput card);
        bool ValidateToken(CardTokenValidationInput card);
    }
}
