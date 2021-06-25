using AutoMapper;
using CustomerCardService.Api.Models.Input;
using CustomerCardService.Api.Models.Output;
using CustomerCardService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerCardService.Core.AutoMapper
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<CardSaveInput, Card>();
            CreateMap<CardTokenValidationInput, Card>();

            CreateMap<Card, CardSaveOutput>();
        }
    }
}
