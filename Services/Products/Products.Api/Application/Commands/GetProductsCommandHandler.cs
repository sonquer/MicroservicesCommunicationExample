using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EasyNetQ;
using MediatR;
using Microsoft.Extensions.Configuration;
using Products.Api.Application.Dtos;
using Products.Api.Application.Models;
using Services.Communication.Users.Requests;
using Services.Communication.Users.Responses;

namespace Products.Api.Application.Commands
{
    public class GetProductsCommandHandler : IRequestHandler<GetProductsCommand, IList<ProductDto>>
    {
        private readonly IConfiguration _configuration;

        public GetProductsCommandHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IList<ProductDto>> Handle(GetProductsCommand request, CancellationToken cancellationToken)
        {
            using var bus = RabbitHutch.CreateBus(_configuration.GetConnectionString("RabbitMq"));

            var products = new List<Product>
            {
                new Product(1, 1, "product 1"),
                new Product(2, 1, "product 2"),
                new Product(3, 2, "product 3"),
            };

            var userIds = products.Select(e => e.UserId)
                .Distinct()
                .ToList();

            try
            {
                var usersResponse = await bus.RequestAsync<GetUsersByIdsRequest, GetUsersByIdsResponse>(new GetUsersByIdsRequest
                {
                    Ids = userIds
                });

                var responsedUsersAsUserDtos = usersResponse.Users.Select(e => new UserDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    Email = e.Email
                });

                return products.Select(e => new ProductDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    UserDto = responsedUsersAsUserDtos.FirstOrDefault(user => user.Id == e.UserId)
                }).ToList();
            }
            catch
            {
                return products.Select(e => new ProductDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    UserDto = null
                }).ToList();
            }
        }
    }
}
