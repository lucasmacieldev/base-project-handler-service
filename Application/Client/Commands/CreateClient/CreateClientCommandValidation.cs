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
                        .WithErrorCode(((int)MessagesEnumerableOrderTag.NameRequired).ToString())
                        .WithMessage(MessagesEnumerableOrderTag.NameRequired.GetDescription());

            RuleFor(x => x.Type)
                .NotNull()
                .NotEmpty()
                .WithErrorCode(((int)MessagesEnumerableOrderTag.TypeClientRequired).ToString())
                .WithMessage(MessagesEnumerableOrderTag.TypeClientRequired.GetDescription());
        }
    }
}
