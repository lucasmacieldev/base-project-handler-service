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

namespace Application.User.Queries.GetToken
{
    public class GetTokenQueryHandler : IRequestHandler<GetTokenQueryRequest, ResponseApiBase<GetTokenQueryResponse>>
    {
        private readonly ILogger<GetTokenQueryHandler> _logger;
        private readonly IContext _context;

        public GetTokenQueryHandler(ILogger<GetTokenQueryHandler> logger, IContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<ResponseApiBase<GetTokenQueryResponse>> Handle(GetTokenQueryRequest request, CancellationToken cancellationToken)
        {
            var response = new ResponseApiBase<GetTokenQueryResponse>();
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.UserName, cancellationToken);
                if (user != null && user.Active)
                {
                    string hashSenha = Helper.Hash(user.Key + request.Password);
                    if (user.Password.Equals(hashSenha))
                    {
                        var issuer = Configuration.JwtIssuer;
                        var audience = Configuration.JwtAudience;
                        var key = Encoding.ASCII.GetBytes(Configuration.JWtKey);
                        var tokenDescriptor = new SecurityTokenDescriptor
                        {
                            Subject = new ClaimsIdentity(new[]
                            {
                        new Claim("Id", Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Sub, request.UserName),
                        new Claim(JwtRegisteredClaimNames.Email, user.Email),
                        new Claim(JwtRegisteredClaimNames.Jti,
                        Guid.NewGuid().ToString())
                    }),
                            Expires = DateTime.UtcNow.AddMinutes(5),
                            Issuer = issuer,
                            Audience = audience,
                            SigningCredentials = new SigningCredentials
                            (new SymmetricSecurityKey(key),
                            SecurityAlgorithms.HmacSha512Signature)
                        };
                        var tokenHandler = new JwtSecurityTokenHandler();
                        var token = tokenHandler.CreateToken(tokenDescriptor);
                        var jwtToken = tokenHandler.WriteToken(token);
                        var stringToken = tokenHandler.WriteToken(token);

                        response.AddSuccess(new GetTokenQueryResponse(stringToken));
                        _logger.LogInformation($"Logged. {request.UserName}");
                        return response;
                    }
                    else
                    {
                        _logger.LogInformation($"Unauthorized. {request.UserName}");
                        response.AddError("Unauthorized");
                        return response;
                    }
                }
                else
                {
                    _logger.LogInformation($"Unauthorized. {request.UserName}");
                    response.AddError("Unauthorized");
                    return response;
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Unauthorized. {ex.Message}");
                response.AddError("Unauthorized");
                return response;
            }
        }
    }
}
