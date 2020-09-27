using FluentValidation;
using HBSIS_Padawan.Sistema.Boletim.Models;
using HBSIS_Padawan.Sistema.Boletim.Models.Enums;
using HBSIS_Padawan.Sistema.Boletim.Validations.Rules;
using System;

namespace HBSIS_Padawan.Sistema.Boletim.Validations
{
    public class AlunoValidation : AbstractValidator<Aluno>
    {
        public AlunoValidation()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("Nome deve ser informado")
                .Must(CharsValidate.Validate).WithMessage("Campo 'Nome' aceita somente letras")
                .Length(3, 20).WithMessage("Campo 'Nome' deve ter no mínimo 3 e no máximo 20 letras");

            RuleFor(x => x.Sobrenome)
                .NotEmpty().WithMessage("Sobrenome deve ser informado")
                .MaximumLength(20).WithMessage("Campo 'Sobrenome' deve ter no máximo 20 caracteres");

            RuleFor(x => x.Nascimento)
                .NotEmpty().WithMessage("Campo 'Data' deve ser informado")
                .Must(x => x.Date < new DateTime(2002, 01, 01)).WithMessage("Data de nascimento não pode ser maior que 01/01/2002");

            RuleFor(x => x.Cpf)
                .NotEmpty().WithMessage("Campo 'CPF' deve ser informado")
                .Must(CPFValidate.Validate).WithMessage("CPF Inválido!");

            RuleForEach(x => x.AlunoCursos)
                 .NotEmpty().WithMessage("Informe um curso para realizar matrícula")
                .ChildRules(y =>
                {
                    y.RuleFor(x => x.Curso.Situacao)
                    .Must(x => x.Equals(Status.Ativo))
                    .WithMessage("Só é possível realizar matrícula em curso com status 'Ativo'");
                });

            RuleForEach(x => x.AlunoMaterias)                
                .ChildRules(y => 
                {
                    y.RuleFor(x => x.Nota)
                    .Must(x => x >= 0 && x <= 100)
                    .WithMessage("Campo 'Nota' deve estar no intervalo [0-100]");
                });
        }
    }
}
