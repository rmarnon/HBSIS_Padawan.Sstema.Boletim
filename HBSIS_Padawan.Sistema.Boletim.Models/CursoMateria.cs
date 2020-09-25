using System.Collections.Generic;

namespace HBSIS_Padawan.Sistema.Boletim.Models
{
    public class CursoMateria
    {
        public long Id { get; set; }        
        public virtual Curso Curso { get; set; }
        public virtual Materia Materia { get; set; }
        public long CursoId { get; set; }
        public long MateriaId { get; set; }
        public ICollection<CursoMateriaAluno> NotasAlunos { get; set; } = new List<CursoMateriaAluno>();
    }
}
