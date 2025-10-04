using App.Application.Features.ProductFeatures.Commands.CreateProduct;
using App.Domain.Concretes.Entities;
using AutoMapper;

namespace App.Persistance.Mappings.Products;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        CreateMap<CreateProductCommand,Product>().ReverseMap();
    }
}
