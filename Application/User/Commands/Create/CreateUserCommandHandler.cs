using Application.Common.Interfaces;
using Application.Common.Models.Response;
using Common;
using MediatR;
using Microsoft.Extensions.Logging;
using Domain.Entities;
using Domain.Enumerators;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Application.User.Commands.Create
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, ResponseApiBase<CreateUserCommandResponse>>
    {
        private readonly ILogger<CreateUserCommandHandler> _logger;
        private readonly IContext _context;

        public CreateUserCommandHandler(ILogger<CreateUserCommandHandler> logger, IContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<ResponseApiBase<CreateUserCommandResponse>> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Request new Client. {request.ToJson()}");
                var response = new ResponseApiBase<CreateUserCommandResponse>();
                Domain.Entities.User user = new Domain.Entities.User();

                user.Name = request.Name;
                user.Email = request.Email;
                user.Document = request.Document;
                user.BirthDate = request.BirthDate;
                user.Type = request.Type;
                user.Active = true;
                user.DocumentProfessional = request.DocumentProssional;

                if (user.Type == UserType.Professional && (user.DocumentProfessional == null || user.DocumentProfessional == ""))
                {
                    _logger.LogInformation($"Documento Profissional é Obrigatorio {request.ToJson()}");
                    response.AddError(MessagesEnumerable.DocumentProfessional.GetHashCode(), MessagesEnumerable.DocumentProfessional.GetDescription());
                    return response;
                }

                var buffer = new byte[20];
                new Random().NextBytes(buffer);
                var password = request.Password;
                var salt = Convert.ToBase64String(buffer);

                user.Key = salt;
                user.Password = Helper.Hash(salt + password);
                await _context.Users.AddAsync(user);

                var result = await _context.SaveChangesAsync(cancellationToken);
                if (result > 0)
                {
                    response.AddSuccess(new CreateUserCommandResponse(user.Id));
                    _logger.LogInformation($"Save new User. {request.ToJson()}");
                    return response;
                }
                else
                {
                    _logger.LogInformation($"Erro to save new User. {request.ToJson()}");
                    response.AddError("erro");
                    return response;
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Erro to save new User. {ex.Message}");
                throw;
            }
        }

        
    }
}
