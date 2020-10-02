using HBSIS_Padawan.Sistema.Boletim.Models;
using HBSIS_Padawan.Sistema.Boletim.Util;

namespace HBSIS_Padawan.Sistema.Boletim.BusinessRule.Interfaces
{
    public interface IStudent
    {
        Result<Aluno> Matricular(Aluno aluno, string curso);
        Result<Aluno> InserirNota(string cpf, string materia, double nota);
        Result<Aluno> AlterarNome(string cpf, string novoNome);
        Result<Aluno> AlterarSobrenome(string cpf, string novoSobrenome);
        Result<Aluno> Excluir(string cpf);
    }
}
