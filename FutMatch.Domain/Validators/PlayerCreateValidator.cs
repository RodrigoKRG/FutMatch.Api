using FluentValidation;
using FutMatch.Domain.Repositories;
using FutMatch.Domain.Requests;

namespace FutMatch.Domain.Validators
{
    public class PlayerCreateValidator : AbstractValidator<PlayerCreateRequest>
    {
        public PlayerCreateValidator(IPlayerRepository playerRepository)
        {
            RuleFor(x => x.Cpf)
             .NotEmpty()
             .WithMessage("CPF is required.")
             .MustAsync(async (cpf, cancellation) => !await playerRepository.ExistsByCpfAsync(cpf))
             .WithMessage("CPF já cadastrado.");
        }
    }
}
