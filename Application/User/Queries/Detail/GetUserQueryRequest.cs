using Application.Common.Models.Response;
using Domain.Enumerators;
using MediatR;

namespace Application.User.Queries.Detail
{
    public class GetUserQueryRequest : IRequest<ResponseApiBase<GetUserQueryResponse>>
    {
        public string Email { get; set; }

        public GetUserQueryRequest(string email)
        {
            Email = email;
        }
    }
}
