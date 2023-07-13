using Application.Common.Models.Error;
using Application.Common.Models.Response;
using Application.User.Commands.Create;
using Application.User.Queries.Detail;
using Application.User.Queries.GetToken;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [ApiController]
    [Route("Users")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator) => _mediator = mediator;

        [AllowAnonymous]
        [HttpPost("/Token")]
        [ProducesResponseType(typeof(ResponseApiBase<GetTokenQueryResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Token([FromBody] GetTokenQueryRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost()]
        [ProducesResponseType(typeof(ResponseApiBase<CreateUserCommandResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateUserCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [Authorize]
        [HttpGet()]
        [ProducesResponseType(typeof(ResponseApiBase<CreateUserCommandResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Detail()
        {
            var request = new GetUserQueryRequest(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
