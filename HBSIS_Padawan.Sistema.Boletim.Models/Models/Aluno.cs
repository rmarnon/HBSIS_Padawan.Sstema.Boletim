using System;
using System.Collections.Generic;

namespace HBSIS_Padawan.Sistema.Boletim.Models
{
    public class Aluno
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public DateTime Nascimento { get; set; }
        public string CPF { get; set; }
        public virtual Curso Curso { get; set; }
        public int CursoId { get; set; }
        public ICollection<CursoMateriaAluno> NotasMaterias { get; set; } = new List<CursoMateriaAluno>();
    }
}
