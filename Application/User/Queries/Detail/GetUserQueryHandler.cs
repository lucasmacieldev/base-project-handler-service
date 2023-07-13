using Application.Common.Interfaces;
using Application.Common.Models.Response;
using Common;
using MediatR;
using Microsoft.Extensions.Logging;
using Domain.Entities;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore;
using Application.User.Queries.GetToken;

namespace Application.User.Queries.Detail
{
    public class GetTokenQueryHandler : IRequestHandler<GetUserQueryRequest, ResponseApiBase<GetUserQueryResponse>>
    {
        private readonly ILogger<GetTokenQueryHandler> _logger;
        private readonly IContext _context;

        public GetTokenQueryHandler(ILogger<GetTokenQueryHandler> logger, IContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<ResponseApiBase<GetUserQueryResponse>> Handle(GetUserQueryRequest request, CancellationToken cancellationToken)
        {
            var response = new ResponseApiBase<GetUserQueryResponse>();
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);
                response.AddSuccess(new GetUserQueryResponse(user));
                _logger.LogInformation($"Logged. {request.Email}");
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"User Not Found. {ex.Message}");
                response.AddError("User Not Found");
                return response;
            }
        }
    }
}
