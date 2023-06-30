using Application.Client.Commands.CreateClient;
using Application.Common.Models.Error;
using Application.Common.Models.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace API.Controllers
{
    [ApiController]
    [Route("Clients")]
    public class ClientController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ClientController(IMediator mediator) => _mediator = mediator;

        [HttpPost()]
        [ProducesResponseType(typeof(ResponseApiBase<CreateClientCommandResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateClientCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPut()]
        [ProducesResponseType(typeof(ResponseApiBase<CreateClientCommandResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status400BadRequest)]
        public IActionResult Update([FromBody] CreateClientCommandRequest tarefa)
        {
            try
            {
                var factory = new ConnectionFactory() { HostName = "localhost" };
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "minhafila1",
                                durable: true,
                                exclusive: false,
                                autoDelete: false,
                                arguments: null);

                    var message = JsonSerializer.Serialize(tarefa);
                    var body = Encoding.UTF8.GetBytes(message);

                    var properties = channel.CreateBasicProperties();
                    properties.Persistent = true;

                    channel.BasicPublish(exchange: "",
                                         routingKey: "minhafila1",
                                         basicProperties: properties,
                                         body: body);
                }

                return Ok(" Tarefa cadastrada na fila");
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
