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
            #region DTO to Model
            CreateMap<TokenInput, Token>();
            CreateMap<CustomerInput, Customer>();
            CreateMap<CardSaveInput, Card>()
                .ForMember(dest => dest.Customer, act => act.MapFrom(from => from.Customer));
            CreateMap<CardTokenValidationInput, Card>()
                .ForMember(dest => dest.Customer, act => act.MapFrom(from => from.Customer))
                .ForMember(dest => dest.Token, act => act.MapFrom(from => from.TokenInput)); ;
            #endregion

            #region Model to DTO
            CreateMap<Token, TokenOutput>();
            CreateMap<Card, CardSaveOutput>()
                .ForMember(dest => dest.Token, act => act.MapFrom(from => from.Token));
            #endregion
        }
    }
}
