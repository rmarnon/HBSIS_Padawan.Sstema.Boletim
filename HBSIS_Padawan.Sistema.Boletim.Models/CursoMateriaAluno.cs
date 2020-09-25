namespace HBSIS_Padawan.Sistema.Boletim.Models
{
    public class CursoMateriaAluno
    {        
        public double Nota { get; set; }       
        public virtual CursoMateria CursoMateria { get; set; }
        public long CursoMateriaId { get; set; }
        public virtual Aluno Aluno  { get; set; }
        public long AlunoId { get; set; }
    }
}
