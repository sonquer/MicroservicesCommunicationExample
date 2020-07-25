using System.Collections.Generic;
using MediatR;
using Users.Api.Application.Models;

namespace Users.Api.Application.Commands
{
    public class GetUsersByIdsCommand : IRequest<IList<User>>
    {
        public List<long> Ids { get; set; }
    }
}
