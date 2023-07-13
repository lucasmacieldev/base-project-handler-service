using Common;
using Domain.Enumerators;
using FluentValidation;

namespace Application.User.Queries.GetToken
{
    public class GetTokenQueryValidation : AbstractValidator<GetTokenQueryRequest>
    {
        public GetTokenQueryValidation()
        {
            RuleFor(x => x.UserName)
                        .NotNull()
                        .NotEmpty()
                        .WithErrorCode(((int)MessagesEnumerable.UserNameRequired).ToString())
                        .WithMessage(MessagesEnumerable.UserNameRequired.GetDescription());

            RuleFor(x => x.Password)
                        .NotNull()
                        .NotEmpty()
                        .WithErrorCode(((int)MessagesEnumerable.PasswordRequired).ToString())
                        .WithMessage(MessagesEnumerable.PasswordRequired.GetDescription());
        }
    }
}
