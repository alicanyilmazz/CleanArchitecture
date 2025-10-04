using App.Application.Common.Concretes.Dtos;
using MediatR;

namespace App.Application.Features.ProductFeatures.Commands.CreateProduct;

public record CreateProductCommand(string Name,string Description,decimal Price,int Quantity) : IRequest<ServiceResult<CreateProductCommandResponse>>;
