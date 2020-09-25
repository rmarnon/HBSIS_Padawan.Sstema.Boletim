using HBSIS_Padawan.Sistema.Boletim.Models.Enums;
using System.Collections.Generic;

namespace HBSIS_Padawan.Sistema.Boletim.Models
{
    public class Curso
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public Status Situação { get; set; }
        public ICollection<Aluno> Alunos { get; set; } = new HashSet<Aluno>();
        public ICollection<CursoMateria> Disciplinas { get; set; } = new List<CursoMateria>();
    }
}
