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
                .Must(NascimentoValidation.Validate).WithMessage("Data de nascimento não pode ser maior que 01/01/2002");

            RuleFor(x => x.Cpf)
                .NotEmpty().WithMessage("Campo 'CPF' deve ser informado")
                .Must(CPFValidate.Validate).WithMessage("CPF Inválido!");

            RuleFor(x => x.Curso)
                .NotEmpty().WithMessage("Curso deve ser informado")
                .Must(x => x.Situacao == Status.Ativo).WithMessage("Aluno só pode ser matriculado em cursos com status 'Ativo'");
        }
    }
}
