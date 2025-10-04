using App.Application.Features.ProductFeatures.Commands.CreateProduct;
using App.Application.Services;
using App.Domain.Concretes.Entities;
using App.Persistance.Context;

namespace App.Persistance.Services;

public class ProductService : IProductService
{
    private readonly AppDbContext _context;
    public ProductService(AppDbContext context)
    {
        _context = context;
    }
    public async Task CreateAsync(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product { Name = request.Name, Description = request.Description, Price = request.Price, Quantity = request.Quantity };
        await _context.Set<Product>().AddAsync(product, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
