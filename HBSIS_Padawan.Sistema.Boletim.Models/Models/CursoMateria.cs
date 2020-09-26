namespace HBSIS_Padawan.Sistema.Boletim.Models
{
    public class CursoMateria
    {
        public int CursoId { get; set; }
        public virtual Curso Curso { get; set; }

        public int MateriaId { get; set; }
        public virtual Materia Materia { get; set; }
    }
}
