using FluentValidation;
using HBSIS_Padawan.Sistema.Boletim.Models;

namespace HBSIS_Padawan.Sistema.Boletim.Validations
{
    public class UsuarioValidation : AbstractValidator<Usuario>
    {
        public UsuarioValidation()
        {
            RuleFor(x => x.Login)
                .NotEmpty().WithMessage("Login deve ser informado!")
                .MaximumLength(50).WithMessage("Login deve ter no máximo 50 caracteres");

            RuleFor(x => x.Senha)
                .NotEmpty().WithMessage("Senha deve ser informada!")
                .Length(8, 50).WithMessage("Senha deve ter no mínimo 8 e no maximo 50 caracteres");

            RuleFor(x => x.Tipo)
                .IsInEnum().WithMessage("Tipo do usuário informado é inválido")
                 .NotNull().WithMessage("Tipo do usuário deve ser informado");
        }
    }
}
