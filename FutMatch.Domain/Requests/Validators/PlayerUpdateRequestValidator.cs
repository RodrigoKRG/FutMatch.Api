using FluentValidation;

namespace FutMatch.Domain.Requests.Validators
{
    public class PlayerUpdateRequestValidator : AbstractValidator<PlayerUpdateRequest>
    {
        public PlayerUpdateRequestValidator()
        {
            RuleFor(user => user.Name)
                .Length(3, 50)
                .WithMessage("O nome deve conter no mínimo 3 caracteres e no maximo 100 caracteres.");

            RuleFor(user => user.Email)
                .EmailAddress()
                .WithMessage("Email não é válido.");

            RuleFor(user => user.BirthDate)
                .LessThan(DateTime.Now)
                .WithMessage("Data de nascimento não pode ser maior que a data atual.");

            RuleFor(user => user.Cpf)
                .Length(11)
                .WithMessage("CPF inválido.");

            RuleFor(user => user.PhoneNumber)
                .Length(11)
                .WithMessage("Telefone inválido.");
        }
    }
}
