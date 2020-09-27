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
                .Must(x => x.Equals(Status.Ativo)).WithMessage("Cadastro permitido apenas para matérias com status 'Ativa'");

            RuleForEach(x => x.CursoMaterias)
                .NotEmpty().WithMessage("Informe uma disciplina para o curso")
                .ChildRules(y =>
                {
                     y.RuleFor(x => x.Materia.Status)                    
                     .Must(x => x.Equals(Status.Ativo))                     
                     .WithMessage("Matéria deve estar com status 'Ativo'");
                });
        }
    }
}
