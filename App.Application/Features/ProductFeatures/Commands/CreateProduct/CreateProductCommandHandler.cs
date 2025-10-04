using App.Application.Common.Concretes.Dtos;
using MediatR;

namespace App.Application.Features.ProductFeatures.Commands.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ServiceResult<CreateProductCommandResponse>>
{
    public async Task<ServiceResult<CreateProductCommandResponse>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        return new ServiceResult<CreateProductCommandResponse>();
    }
}
