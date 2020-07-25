using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Users.Api.Application.Commands;
using Users.Api.Application.Models;

namespace Users.Api.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<User>), 200)]
        public async Task<IActionResult> Get([FromQuery] long[] ids, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetUsersByIdsCommand
            {
                Ids = ids.ToList()
            }, cancellationToken);

            return Ok(result);
        }
    }
}
