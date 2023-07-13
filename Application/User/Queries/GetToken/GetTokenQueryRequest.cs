using Application.Common.Models.Response;
using Domain.Enumerators;
using MediatR;

namespace Application.User.Queries.GetToken
{
    public class GetTokenQueryRequest : IRequest<ResponseApiBase<GetTokenQueryResponse>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
