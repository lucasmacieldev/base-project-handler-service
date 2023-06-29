using Application.Common.Interfaces;
using Application.Common.Models.Response;
using Common;
using MediatR;
using Microsoft.Extensions.Logging;
using Domain.Entities;

namespace Application.Client.Commands.CreateClient
{
    public class CreateClientCommandHandler : IRequestHandler<CreateClientCommandRequest, ResponseApiBase<CreateClientCommandResponse>>
    {
        private readonly ILogger<CreateClientCommandHandler> _logger;
        private readonly IContext _context;

        public CreateClientCommandHandler(ILogger<CreateClientCommandHandler> logger, IContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<ResponseApiBase<CreateClientCommandResponse>> Handle(CreateClientCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Request new Client. {request.ToJson()}");
                var response = new ResponseApiBase<CreateClientCommandResponse>();
                var newClient = new Domain.Entities.Client();
                newClient.Street = "qlqr";
                newClient.DateBirth = DateTime.Now;
                newClient.Name = "qlqr 1 nome";
                newClient.ZipCode = "zip";
                await _context.Clients.AddAsync(newClient);

                var result = await _context.SaveChangesAsync(cancellationToken);
                if (result > 0)
                {
                    response.AddSuccess(new CreateClientCommandResponse(newClient.Id));
                    _logger.LogInformation($"Save new Client. {request.ToJson()}");
                    return response;
                }
                else
                {
                    _logger.LogInformation($"Erro to save new Client. {request.ToJson()}");
                    response.AddError("erro");
                    return response;
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Erro to save new Client. {ex.Message}");
                throw;
            }
        }
    }
}
