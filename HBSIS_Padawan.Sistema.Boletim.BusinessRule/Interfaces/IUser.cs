using HBSIS_Padawan.Sistema.Boletim.Models;
using HBSIS_Padawan.Sistema.Boletim.Models.Enums;
using HBSIS_Padawan.Sistema.Boletim.Util;

namespace HBSIS_Padawan.Sistema.Boletim.BusinessRule.Interfaces
{
    public interface IUser
    {
        Result<Usuario> Conectar(string login, string senha);
        Result<Usuario> Cadastrar(string login, string senha, TipoUsuario tipo);
        Result<Usuario> Excluir(string login, string senha);
        Result<Usuario> AlterarLogin(string login, string novoLogin, string senha);
        Result<Usuario> AlteraSenha(string login, string senha, string novaSenha);
    }
}
