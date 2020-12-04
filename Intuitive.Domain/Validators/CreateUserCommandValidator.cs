using FluentValidation;
using Intuitive.Domain.Commands;

namespace Intuitive.Domain.Validators
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(o => o.Name).NotEmpty().WithMessage("Name is required").WithErrorCode("1001");

            RuleFor(o => o.DtNasc).NotEmpty();

            RuleFor(o => o.Email).NotEmpty();

            RuleFor(o => o.Username).NotEmpty();

            RuleFor(x => x.Password).Length(0, 10);
        }

        /// <summary>
        /// TODO:Documentar
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static bool IsValidType(string type)
        {
            var isValid = type.Equals("EMPLOYEE", System.StringComparison.OrdinalIgnoreCase) || type.Equals("TRAINEE", System.StringComparison.OrdinalIgnoreCase);
            return isValid;
        }

    }
}
