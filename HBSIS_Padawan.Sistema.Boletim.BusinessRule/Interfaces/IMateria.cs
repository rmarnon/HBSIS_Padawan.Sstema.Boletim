using HBSIS_Padawan.Sistema.Boletim.Models;
using HBSIS_Padawan.Sistema.Boletim.Models.Enums;
using HBSIS_Padawan.Sistema.Boletim.Util;

namespace HBSIS_Padawan.Sistema.Boletim.BusinessRule.Interfaces
{
    public interface IMateria
    {       
        Result<Materia> Cadastrar(Materia materia);       
        Result<Materia> Alteracoes(string nome, string novoNome, string descricao);
        Result<Materia> AlteraStatus(string nome, Status status);
        Result<Materia> Excluir(string nome);        
    }
}
