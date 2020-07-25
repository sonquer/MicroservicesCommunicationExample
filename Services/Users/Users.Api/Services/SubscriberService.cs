using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EasyNetQ;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Services.Communication.Users.Models;
using Services.Communication.Users.Requests;
using Services.Communication.Users.Responses;
using Users.Api.Application.Commands;

namespace Users.Api.Services
{
    public class SubscriberService : IHostedService
    {
        private readonly IConfiguration _configuration;

        private readonly IMediator _mediator;

        public SubscriberService(IConfiguration configuration, IMediator mediator)
        {
            _configuration = configuration;
            _mediator = mediator;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await RunAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        private async Task RunAsync(CancellationToken cancellationToken)
        {
            try
            {
                using var bus = RabbitHutch.CreateBus(_configuration.GetConnectionString("RabbitMq"));
                using var responder = bus.RespondAsync<GetUsersByIdsRequest, GetUsersByIdsResponse>(async request =>
                {
                    var users = await _mediator.Send(new GetUsersByIdsCommand
                    {
                        Ids = request.Ids
                    }, cancellationToken);
                    return new GetUsersByIdsResponse
                    {
                        Users = users.Select(e => new UserModel
                        {
                            Id = e.Id,
                            Email = e.Email,
                            Name = e.Name
                        }).ToList()
                    };
                });

                while (cancellationToken.IsCancellationRequested == false)
                {
                    await Task.Delay(5000, cancellationToken);
                }
            }
            catch
            {
                await RunAsync(cancellationToken)
                    .ConfigureAwait(false);
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
        }
    }
}
