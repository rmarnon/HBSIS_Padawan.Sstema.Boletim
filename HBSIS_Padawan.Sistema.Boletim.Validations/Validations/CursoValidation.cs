using FluentValidation;
using HBSIS_Padawan.Sistema.Boletim.Models;
using HBSIS_Padawan.Sistema.Boletim.Models.Enums;
using HBSIS_Padawan.Sistema.Boletim.Validations.Rules;

namespace HBSIS_Padawan.Sistema.Boletim.Validations
{
    public class CursoValidation : AbstractValidator<Curso>
    {
        public CursoValidation()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("Curso deve ser informado")
                .Must(CharsValidate.Validate).WithMessage("Campo 'Curso' aceita apenas letras")
                .Length(3, 50).WithMessage("Campo 'Curso' deve ter no mínimo 3 e no máximo 50 letras");

            RuleFor(x => x.Situacao)
                .NotEmpty().WithMessage("Situação do curso não foi informado")
                .When(x => x.Situacao == Status.Ativo).WithMessage("Cadastro permitido apenas para matérias com status 'Ativa'");

            RuleFor(x => x.Disciplinas)
                .NotEmpty().WithMessage("Cadastro de curso está condicionado a ter pelo menos uma disciplina");                
        }
    }
}
