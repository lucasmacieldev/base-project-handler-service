using Application.Common.Models.Response;
using Domain.Enumerators;
using MediatR;

namespace Application.User.Commands.Create
{
    public class CreateUserCommandRequest : IRequest<ResponseApiBase<CreateUserCommandResponse>>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Document { get; set; }
        public string Password { get; set; }
        public DateTime BirthDate { get; set; }
        public UserType Type { get; set; }
        public string DocumentProssional { get; set; }
    }
}
