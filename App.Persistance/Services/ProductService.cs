using App.Application.Features.ProductFeatures.Commands.CreateProduct;
using App.Application.Services;
using App.Domain.Concretes.Entities;
using App.Persistance.Context;
using AutoMapper;

namespace App.Persistance.Services;

public class ProductService : IProductService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    public ProductService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task CreateAsync(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = _mapper.Map<Product>(request);
        await _context.Set<Product>().AddAsync(product, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
