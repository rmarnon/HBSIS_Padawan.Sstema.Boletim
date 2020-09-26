using FluentValidation;
using HBSIS_Padawan.Sistema.Boletim.Models;
using HBSIS_Padawan.Sistema.Boletim.Validations.Rules;

namespace HBSIS_Padawan.Sistema.Boletim.Validations
{
    public class MateriaValidation : AbstractValidator<Materia>
    {
        public MateriaValidation()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("Nome deve ser informado")
                .MaximumLength(50).WithMessage("Campo 'Nome' permite no máximo 50 caracteres");

            RuleFor(x => x.Descricao)
                .NotEmpty().WithMessage("Descrição deve ser informada")
                .Must(CharsValidate.Validate).WithMessage("Campo 'Descrição' aceita apenas letras");

            RuleFor(x => x.Cadastro)
                .NotEmpty().WithMessage("Data de cadastro deve ser informado")
                .Must(CadastroValidation.Validate).WithMessage("Data de cadastro não pode ser datas futuras");

            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Status informado não é válido")
                .NotNull().WithMessage("Status da matéria deve ser informado")
                .Must(StatusMateriaValidation.Validate).WithMessage("Status permitidos [Ativo|Inativo]");
        }
    }
}
