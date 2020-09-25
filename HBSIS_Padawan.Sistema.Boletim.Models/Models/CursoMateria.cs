using System.Collections.Generic;

namespace HBSIS_Padawan.Sistema.Boletim.Models
{
    public class CursoMateria
    {
        public int Id { get; set; }
        public virtual Curso Curso { get; set; }
        public virtual Materia Materia { get; set; }
        public int CursoId { get; set; }
        public int MateriaId { get; set; }
        public ICollection<CursoMateriaAluno> NotasAlunos { get; set; } = new List<CursoMateriaAluno>();
    }
}
