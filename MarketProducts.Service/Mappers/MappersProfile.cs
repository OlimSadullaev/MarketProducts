using AutoMapper;
using MarketProducts.Domain.Entities.Products;
using MarketProducts.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketProducts.Service.Mappers;

public class MappersProfile : Profile
{
    public MappersProfile()
    {
        CreateMap<Product, ProductForCreationDto>().ReverseMap();
    }   
}

