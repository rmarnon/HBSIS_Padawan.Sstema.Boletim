using HBSIS_Padawan.Sistema.Boletim.BusinessRule.Interfaces;
using HBSIS_Padawan.Sistema.Boletim.Models;
using Microsoft.AspNetCore.Mvc;

namespace HBSIS_Padawan.Sistema.Boletim.API.Controllers
{
    [ApiController]
    [Route("Usuario")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUser usuario;        

        public UsuarioController(IUser user) => usuario = user;

        [HttpPost]
        [Route("Conecta")]
        public ActionResult Conectar(string login, string senha) => Ok(usuario.Conectar(login, senha));        

        [HttpPost]
        [Route("Cadastra")]
        public ActionResult Cadastrar(Usuario user) => Ok(usuario.Cadastrar(user.Login, user.Senha, user.Tipo));        

        [HttpDelete]
        [Route("Deleta")]
        public ActionResult Deletar(string login, string senha) => Ok(usuario.Excluir(login, senha));

        [HttpPut]
        [Route("AlteraSenha")]
        public ActionResult AlteraSenha(string login, string senha, string novaSenha) => Ok(usuario.AlteraSenha(login, senha, novaSenha));

        [HttpPut]
        [Route("AlteraLogin")]
        public ActionResult AlteraLogin(string login, string novoLogin, string senha) => Ok(usuario.AlterarLogin(login, novoLogin, senha));
    }
}
