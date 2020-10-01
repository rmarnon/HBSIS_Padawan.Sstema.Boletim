using HBSIS_Padawan.Sistema.Boletim.BusinessRule.Interfaces;
using HBSIS_Padawan.Sistema.Boletim.Models;
using HBSIS_Padawan.Sistema.Boletim.Models.Enums;
using Microsoft.AspNetCore.Mvc;

namespace HBSIS_Padawan.Sistema.Boletim.API.Controllers
{

    [ApiController]
    [Route("Curso")]
    public class CursoController : ControllerBase
    {
        private readonly ICourse curso;
        public CursoController(ICourse icourse) => curso = icourse;

        [HttpPost]
        [Route("Cadastra")]
        public ActionResult Cadastra(Curso course) => Ok(curso.Cadastar(course));

        [HttpPut]
        [Route("AlteraSituacao")]
        public ActionResult AlteraSituacao(string nome, Status situacao) => Ok(curso.AlteraSituacao(nome, situacao));

        [HttpPut]
        [Route("cadastraMateria")]
        public ActionResult CadastraMateria(string nomeCurso, string nomeMateria) 
            => Ok(curso.CadastraMateria(nomeCurso, nomeMateria));

        [HttpDelete]
        [Route("Deleta")]
        public ActionResult Excluir(string nome) => Ok(curso.Excluir(nome));
    }
}
