using Common;
using Domain.Enumerators;
using FluentValidation;

namespace Application.Client.Commands.CreateClient
{
    public class CreateClientCommandValidation : AbstractValidator<CreateClientCommandRequest>
    {
        public CreateClientCommandValidation()
        {
            RuleFor(x => x.Nome)
                        .NotNull()
                        .NotEmpty()
                        .WithErrorCode(((int)MessagesEnumerable.NameRequired).ToString())
                        .WithMessage(MessagesEnumerable.NameRequired.GetDescription());

            RuleFor(x => x.Type)
                .NotNull()
                .NotEmpty()
                .WithErrorCode(((int)MessagesEnumerable.TypeClientRequired).ToString())
                .WithMessage(MessagesEnumerable.TypeClientRequired.GetDescription());
        }
    }
}
