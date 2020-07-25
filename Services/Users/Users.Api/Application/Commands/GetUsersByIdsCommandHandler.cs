using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Users.Api.Application.Models;

namespace Users.Api.Application.Commands
{
    public class GetUsersByIdsCommandHandler : IRequestHandler<GetUsersByIdsCommand, IList<User>>
    {
        public async Task<IList<User>> Handle(GetUsersByIdsCommand request, CancellationToken cancellationToken)
        {
            if (request?.Ids is null)
            {
                return new List<User>();
            }

            var users = new List<User>
            {
                new User(1, "user@example.com", "example", DateTime.Now),
                new User(2, "test@example.com", "test", DateTime.Now),
                new User(3, "secret@example.com", "secret", DateTime.Now)
            };

            return users.Where(e => request.Ids.Contains(e.Id))
                .ToList();
        }
    }
}
