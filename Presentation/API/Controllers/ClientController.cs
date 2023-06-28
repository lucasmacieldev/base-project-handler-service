using Application.Common.Models.Error;
using Application.Common.Models.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("Clients")]
    public class ClientController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ClientController(IMediator mediator) => _mediator = mediator;

        //[HttpGet()]
        //[ProducesResponseType(typeof(ResponseApiBase<IEnumerable<VoucherListQueryResponse>>), StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(ResponseError), StatusCodes.Status400BadRequest)]
        //public async Task<IActionResult> Vouchers([FromQuery] VoucherListQueryRequest request)
        //{
        //    var response = await _mediator.Send(request);
        //    return Ok(response);
        //}

    }
}