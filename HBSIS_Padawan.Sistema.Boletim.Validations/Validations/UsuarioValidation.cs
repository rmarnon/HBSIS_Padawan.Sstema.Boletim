using FluentValidation;
using HBSIS_Padawan.Sistema.Boletim.Models;
using HBSIS_Padawan.Sistema.Boletim.Models.Enums;

namespace HBSIS_Padawan.Sistema.Boletim.Validations
{
    public class UsuarioValidation : AbstractValidator<Usuario>
    {
        public UsuarioValidation()
        {
            RuleFor(x => x.Login)
                .NotEmpty().WithMessage("Login deve ser informado!")
                .Length(3, 50).WithMessage("Login deve ter no mínimo 3 e no  máximo 50 caracteres");

            RuleFor(x => x.Senha)
                .NotEmpty().WithMessage("Senha deve ser informada!")
                .Length(8, 50).WithMessage("Senha deve ter no mínimo 8 e no maximo 50 caracteres");
                        
            RuleFor(x => x.Tipo)
            .IsInEnum().WithMessage("Tipo do usuário informado é inválido")            
            .When(x => x.Tipo is TipoUsuario && x.Tipo > 0);

        }
    }
}
