using App.Application.Features.ProductFeatures.Commands.CreateProduct;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace App.Presentation.Controllers
{
    public class ProductController : ApiController
    {
        public ProductController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductCommand request, CancellationToken cancellationToken) => CreateActionResult(await _mediator.Send(request, cancellationToken));

    }
}
