using HBSIS_Padawan.Sistema.Boletim.Models;
using HBSIS_Padawan.Sistema.Boletim.Models.Enums;
using HBSIS_Padawan.Sistema.Boletim.Util;

namespace HBSIS_Padawan.Sistema.Boletim.BusinessRule.Interfaces
{
    public interface ICourse
    {
        Result<Curso> Cadastar(Curso curso);
        Result<Curso> AlteraSituacao(string curso, Status status);
        Result<Curso> CadastraMateria(string curso, string materia);
        Result<Curso> Excluir(string nome);
    }
}
