namespace HBSIS_Padawan.Sistema.Boletim.Models.Models
{
    public class AlunoMateria
    {
        public int Id { get; set; }
        public double Nota { get; set; }
        public int AlunoId { get; set; }
        public virtual Aluno Aluno { get; set; }

        public int MateriaId { get; set; }
        public virtual Materia Materia { get; set; }
    }
}
