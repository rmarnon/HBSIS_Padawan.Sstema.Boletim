using FluentValidation;
using HBSIS_Padawan.Sistema.Boletim.Models;
using HBSIS_Padawan.Sistema.Boletim.Models.Enums;
using HBSIS_Padawan.Sistema.Boletim.Validations.Rules;
using Microsoft.VisualBasic;

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
                .When(x => x.Cadastro < DateAndTime.Now).WithMessage("Data de cadastro não pode ser datas futuras");

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Status da matéria deve ser informado")
                .When(x => x.Status == Status.Ativo || x.Status == Status.Inativo).WithMessage("Status permitidos [Ativo|Inativo]");
        }
    }
}
