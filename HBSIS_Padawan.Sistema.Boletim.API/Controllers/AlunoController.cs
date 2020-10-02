using HBSIS_Padawan.Sistema.Boletim.BusinessRule.Interfaces;
using HBSIS_Padawan.Sistema.Boletim.Models;
using Microsoft.AspNetCore.Mvc;

namespace HBSIS_Padawan.Sistema.Boletim.API.Controllers
{
    [ApiController]
    [Route("Aluno")]    
    public class AlunoController : ControllerBase
    {
        private readonly IStudent student;
        public AlunoController(IStudent aluno) => student = aluno;

        [HttpPost]
        [Route("Matricula")]
        public ActionResult Matricula(Aluno aluno, string nomeCurso) => Ok(student.Matricular(aluno, nomeCurso));

        [HttpPut]
        [Route("InsereNota")]
        public ActionResult InsereNota(string cpf, string nomeMateria, double nota)
            => Ok(student.InserirNota(cpf, nomeMateria, nota));

        [HttpPut]
        [Route("AlteraNome")]
        public ActionResult AlteraNome(string cpf, string novoNome) => Ok(student.AlterarNome(cpf, novoNome));

        [HttpPut]
        [Route("AlteraSobrenome")]
        public ActionResult Alterasobrenome(string cpf, string novoSobrenome) => Ok(student.AlterarSobrenome(cpf, novoSobrenome));

        [HttpDelete]
        [Route("Exclui")]
        public ActionResult Exclui(string nome) => Ok(student.Excluir(nome));
    }
}
