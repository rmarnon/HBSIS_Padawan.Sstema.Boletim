using System;
using System.Collections.Generic;

namespace HBSIS_Padawan.Sistema.Boletim.Models
{
    public class Aluno
    {
        public long Id { get; set; }
        public long CursoId { get; set; }        
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Cpf { get; set; }        
        public DateTime Nascimento { get; set; }
        public virtual Curso Curso { get; set; }
        public ICollection<CursoMateriaAluno> NotasMaterias { get; set; } = new List<CursoMateriaAluno>();
    }
}
