using FluentValidation;
using HBSIS_Padawan.Sistema.Boletim.Models;
using HBSIS_Padawan.Sistema.Boletim.Repository.Data;

namespace HBSIS_Padawan.Sistema.Boletim.Validations
{
    public class NotaValidation : AbstractValidator<CursoMateriaAluno>
    {
        private readonly ApplicationContext db = new ApplicationContext();
        public NotaValidation()
        {
            RuleFor(x => x.Nota)
                .NotEmpty().WithMessage("Nota deve ser informada")
                .When(x => x.Nota >= 0 && x.Nota <= 100).WithMessage("Informar nota [0-100]");

            RuleFor(x => x.Aluno)
                .NotEmpty().WithMessage("Aluno deve ser informado")
                .When(x => x.Aluno.Equals(db.Alunos)).WithMessage("Aluno deve estar cadastrado");

            RuleFor(x => x.CursoMateria)
                .NotEmpty().WithMessage("Matéria deve ser informada")
                .When(x => x.CursoMateria.Materia.Equals(db.Materias)).WithMessage("Matéria deve estar cadastrada");
        }
    }
}
