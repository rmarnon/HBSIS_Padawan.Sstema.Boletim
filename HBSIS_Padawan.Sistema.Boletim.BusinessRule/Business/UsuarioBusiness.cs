using HBSIS_Padawan.Sistema.Boletim.BusinessRule.Exceptions;
using HBSIS_Padawan.Sistema.Boletim.BusinessRule.Interfaces;
using HBSIS_Padawan.Sistema.Boletim.Models;
using HBSIS_Padawan.Sistema.Boletim.Models.Enums;
using HBSIS_Padawan.Sistema.Boletim.Repositories.Data;
using HBSIS_Padawan.Sistema.Boletim.Util;
using System.Linq;
using System.Net;

namespace HBSIS_Padawan.Sistema.Boletim.BusinessRule
{
    public class UsuarioBusiness : IUser
    {
        private readonly ApplicationContext db;
        Result<Usuario> result = new Result<Usuario>();
        Usuario user = new Usuario();

        public UsuarioBusiness(ApplicationContext context) => db = context;

        public Result<Usuario> AlterarLogin(string login, string novoLogin, string senha)
        {
            try
            {
                using (db)
                {
                    var valido = Retorno.ValidaEntrada(new Usuario { Login = novoLogin, Senha = senha, Tipo = TipoUsuario.Aluno });                    

                    if (!valido.IsValid)
                        return Retorno.NaoValidaUsuario(valido);

                    user = db.Usuarios.FirstOrDefault(x => x.Login == login);

                    if (user is null)
                        return Retorno.NaoEncontradoUsuario();
                    
                    if (user.Senha != senha)
                        return Retorno.SenhaInvalida();

                    user.Login = novoLogin;
                    db.SaveChanges();

                    return Retorno.Ok(user);
                }
            }
            catch (BusinessException e)
            {
                return RetornaErrosDesconhecidos(e);
            }
        }

        public Result<Usuario> AlteraSenha(string login, string senha, string novaSenha)
        {
            try
            {
                using (db)
                {
                    var valido = Retorno.ValidaEntrada(new Usuario { Login = login, Senha = novaSenha });

                    if (!valido.IsValid)
                        return Retorno.NaoValidaUsuario(valido);

                    user = db.Usuarios.FirstOrDefault(x => x.Login == login);

                    if (user is null)
                        return Retorno.NaoEncontradoUsuario();

                    if (user.Senha != senha)
                        return Retorno.SenhaInvalida();

                    user.Senha = novaSenha;
                    db.SaveChanges();

                    return Retorno.Ok(user);
                }
            }
            catch (BusinessException e)
            {
                return RetornaErrosDesconhecidos(e);
            }
        }

        public Result<Usuario> Cadastrar(Usuario user)
        {
            try
            {
                var valido = Retorno.ValidaEntrada(user);

                if (!valido.IsValid)
                    return Retorno.NaoValidaUsuario(valido);

                using (db)
                {
                    foreach (var usuario in db.Usuarios)
                    {
                        if (usuario.Login == user.Login)
                        {
                            result.Error = true;
                            result.Message.Add("Usuário já está cadastrado");
                            result.Status = HttpStatusCode.BadRequest;
                            return result;
                        }
                    }
                                        
                    db.Usuarios.Add(user);
                    db.SaveChanges();

                    return Retorno.Ok(user);
                }
            }
            catch (BusinessException e)
            {
                return RetornaErrosDesconhecidos(e);
            }
        }

        public Result<Usuario> Conectar(string login, string senha)
        {
            try
            {
                using (db)
                {
                    user = db.Usuarios.FirstOrDefault(x => x.Login == login);

                    if (user is null)
                        return Retorno.NaoEncontradoUsuario();

                    var valido = Retorno.ValidaEntrada(new Usuario { Login = login, Senha = senha, Tipo = user.Tipo });

                    if (!valido.IsValid)
                        return Retorno.NaoValidaUsuario(valido);

                    if (user.Senha != senha)
                        return Retorno.SenhaInvalida();

                    return Retorno.Ok(user);
                }
            }
            catch (BusinessException e)
            {
                return RetornaErrosDesconhecidos(e);
            }
        }

        public Result<Usuario> Excluir(string login, string senha)
        {
            try
            {
                using (db)
                {
                    user = db.Usuarios.FirstOrDefault(x => x.Login == login);

                    if (user is null)
                        return Retorno.NaoEncontradoUsuario();

                    var valido = Retorno.ValidaEntrada(new Usuario { Login = login, Senha = senha, Tipo = user.Tipo });

                    if (!valido.IsValid)
                        return Retorno.NaoValidaUsuario(valido);

                    if (user.Senha != senha)
                        return Retorno.SenhaInvalida();

                    db.Usuarios.Remove(user);
                    db.SaveChanges();

                    return Retorno.Ok(user);
                }
            }
            catch (BusinessException e)
            {
                return RetornaErrosDesconhecidos(e);
            }
        }

        private Result<Usuario> RetornaErrosDesconhecidos(BusinessException e)
        {
            result.Error = true;
            result.Message.Add(e.Message);
            result.Status = HttpStatusCode.InternalServerError;
            return result;
        }
    }
}
