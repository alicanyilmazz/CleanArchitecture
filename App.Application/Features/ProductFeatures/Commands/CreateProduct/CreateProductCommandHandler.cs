using App.Application.Common.Concretes.Dtos;
using App.Application.Services;
using MediatR;

namespace App.Application.Features.ProductFeatures.Commands.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ServiceResult<CreateProductCommandResponse>>
{
    private readonly IProductService _productService;
    public CreateProductCommandHandler(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<ServiceResult<CreateProductCommandResponse>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        await _productService.CreateAsync(request, cancellationToken);
        return ServiceResult<CreateProductCommandResponse>.Success(new CreateProductCommandResponse());
    }
}
