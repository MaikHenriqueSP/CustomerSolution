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
            CreateMap<CustomerInput, Customer>();
            CreateMap<CardSaveInput, Card>()
                .ForMember(dest => dest.Customer, act => act.MapFrom(from => from.Customer));
            CreateMap<CardTokenValidationInput, Card>()
                .ForMember(dest => dest.Customer, act => act.MapFrom(from => from.Customer));

            CreateMap<Card, CardSaveOutput>();
        }
    }
}
