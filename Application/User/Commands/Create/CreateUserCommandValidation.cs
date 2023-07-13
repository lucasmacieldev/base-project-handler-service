using Common;
using Domain.Enumerators;
using FluentValidation;

namespace Application.User.Commands.Create
{
    public class CreateUserCommandValidation : AbstractValidator<CreateUserCommandRequest>
    {
        public CreateUserCommandValidation()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .WithErrorCode(((int)MessagesEnumerable.NameRequired).ToString())
                .WithMessage(MessagesEnumerable.NameRequired.GetDescription());

            RuleFor(x => x.BirthDate)
                .NotNull()
                .NotEmpty()
                .WithErrorCode(((int)MessagesEnumerable.BirthDate).ToString())
                .WithMessage(MessagesEnumerable.BirthDate.GetDescription());
            
            RuleFor(x => x.Email)
                .NotNull()
                .NotEmpty()
                .WithErrorCode(((int)MessagesEnumerable.EmailRequired).ToString())
                .WithMessage(MessagesEnumerable.EmailRequired.GetDescription());

            RuleFor(x => x.Password)
                .NotNull()
                .NotEmpty()
                .WithErrorCode(((int)MessagesEnumerable.EmailRequired).ToString())
                .WithMessage(MessagesEnumerable.EmailRequired.GetDescription());

            RuleFor(x => x.Document)
                .NotNull()
                .NotEmpty()
                .WithErrorCode(((int)MessagesEnumerable.DocumentRequired).ToString())
                .WithMessage(MessagesEnumerable.DocumentRequired.GetDescription());

            RuleFor(x => x.Type)
                .NotNull()
                .NotEmpty()
                .WithErrorCode(((int)MessagesEnumerable.TypeClientRequired).ToString())
                .WithMessage(MessagesEnumerable.TypeClientRequired.GetDescription());
        }
    }
}
