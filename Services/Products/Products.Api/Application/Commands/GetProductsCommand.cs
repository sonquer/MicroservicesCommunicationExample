using System.Collections.Generic;
using MediatR;
using Products.Api.Application.Dtos;

namespace Products.Api.Application.Commands
{
    public class GetProductsCommand : IRequest<IList<ProductDto>>
    {
    }
}
