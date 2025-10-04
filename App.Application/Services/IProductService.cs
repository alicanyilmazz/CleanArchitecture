using App.Application.Features.ProductFeatures.Commands.CreateProduct;

namespace App.Application.Services;

public interface IProductService
{
    Task CreateAsync(CreateProductCommand request,CancellationToken cancellationToken);
}
