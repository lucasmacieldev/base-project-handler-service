using Application.Common.Models.Response;
using Domain.Enumerators;
using MediatR;

namespace Application.Client.Commands.CreateClient
{
    public class CreateClientCommandRequest : IRequest<ResponseApiBase<CreateClientCommandResponse>>
    {
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public DateTime DataNascimento { get; set; }
        public ClientType Type { get; set; }
    }
}
