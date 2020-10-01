using HBSIS_Padawan.Sistema.Boletim.BusinessRule.Interfaces;
using HBSIS_Padawan.Sistema.Boletim.Models;
using HBSIS_Padawan.Sistema.Boletim.Models.Enums;
using Microsoft.AspNetCore.Mvc;

namespace HBSIS_Padawan.Sistema.Boletim.API.Controllers
{
    [ApiController]
    [Route("Materia")]    
    public class MateriaController : ControllerBase
    {
        private readonly IMateria materia;

        public MateriaController(IMateria imateria) => materia = imateria;

        [HttpPost]
        [Route("Cadastra")]
        public ActionResult Cadastra(Materia _materia) => Ok(materia.Cadastrar(_materia));

        [HttpPut]
        [Route("Alteracoes")]
        public ActionResult Alteracoes(string nome, string novoNome, string descricao) 
            => Ok(materia.Alteracoes(nome, novoNome, descricao));

        [HttpPut]
        [Route("AlteraStatus")]
        public ActionResult AlteraStatus(string nome, Status status) => Ok(materia.AlteraStatus(nome, status));

        [HttpDelete]
        [Route("Deleta")]
        public ActionResult Exclui(string nome) => Ok(materia.Excluir(nome));

    }
}
