using FluentValidation;

namespace FutMatch.Domain.Requests.Validators
{
    public class TeamCreateRequestValidator : AbstractValidator<TeamCreateRequest>
    {
        public TeamCreateRequestValidator()
        {
            RuleFor(team => team.Name)
                .NotEmpty()
                .WithMessage("Nome não pode ser vazio.")
                .Length(3, 50)
                .WithMessage("O nome deve conter no mínimo 3 caracteres e no maximo 100 caracteres.");
        }
    }
}
