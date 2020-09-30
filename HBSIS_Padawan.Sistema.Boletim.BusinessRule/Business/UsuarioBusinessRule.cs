using FluentValidation.Results;
using HBSIS_Padawan.Sistema.Boletim.BusinessRule.Exceptions;
using HBSIS_Padawan.Sistema.Boletim.BusinessRule.Interfaces;
using HBSIS_Padawan.Sistema.Boletim.Models;
using HBSIS_Padawan.Sistema.Boletim.Models.Enums;
using HBSIS_Padawan.Sistema.Boletim.Repositories.Data;
using HBSIS_Padawan.Sistema.Boletim.Util;
using HBSIS_Padawan.Sistema.Boletim.Validations;
using System.Linq;
using System.Net;

namespace HBSIS_Padawan.Sistema.Boletim.BusinessRule
{
    public class UsuarioBusinessRule : IUser
    {
        private readonly ApplicationContext db;
        Result<Usuario> result = new Result<Usuario>();
        Usuario user = new Usuario();

        public UsuarioBusinessRule(ApplicationContext context) => db = context;

        public Result<Usuario> AlterarLogin(string login, string novoLogin, string senha)
        {
            try
            {
                using (db)
                {
                    var valido = ValidaEntrada(new Usuario { Login = novoLogin, Senha = senha, Tipo = TipoUsuario.Aluno });                    

                    if (!valido.IsValid)
                        return RetornaNaoValido(valido);

                    user = db.Usuarios.FirstOrDefault(x => x.Login == login);

                    if (user is null)
                        return RetornaUsuarioNaoEncontrado();
                    
                    if (user.Senha != senha)
                        return RetornaSenhaInvalida();

                    user.Login = novoLogin;
                    db.SaveChanges();

                    return RetornaOk();
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
                    var valido = ValidaEntrada(new Usuario { Login = login, Senha = novaSenha });

                    if (!valido.IsValid)
                        return RetornaNaoValido(valido);

                    user = db.Usuarios.FirstOrDefault(x => x.Login == login);

                    if (user is null)
                        return RetornaUsuarioNaoEncontrado();

                    if (user.Senha != senha)
                        return RetornaSenhaInvalida();

                    user.Senha = novaSenha;
                    db.SaveChanges();

                    return RetornaOk();
                }
            }
            catch (BusinessException e)
            {
                return RetornaErrosDesconhecidos(e);
            }
        }

        public Result<Usuario> Cadastrar(string login, string senha, TipoUsuario tipo)
        {
            try
            {
                user.Login = login;
                user.Senha = senha;
                user.Tipo = tipo;

                var valido = ValidaEntrada(user);

                if (!valido.IsValid)
                    return RetornaNaoValido(valido);

                using (db)
                {
                    foreach (var usuario in db.Usuarios)
                    {
                        if (usuario.Login == login)
                        {
                            result.Error = true;
                            result.Message.Add("Usuário já está cadastrado");
                            result.Status = HttpStatusCode.BadRequest;
                            return result;
                        }
                    }

                    db.Usuarios.Add(user);
                    db.SaveChanges();

                    return RetornaOk();
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
                        return RetornaUsuarioNaoEncontrado();

                    var valido = ValidaEntrada(new Usuario { Login = login, Senha = senha, Tipo = user.Tipo });

                    if (!valido.IsValid)
                        return RetornaNaoValido(valido);

                    if (user.Senha != senha)
                        return RetornaSenhaInvalida();

                    return RetornaOk();
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
                        return RetornaUsuarioNaoEncontrado();

                    var valido = ValidaEntrada(new Usuario { Login = login, Senha = senha, Tipo = user.Tipo });

                    if (!valido.IsValid)
                        return RetornaNaoValido(valido);

                    if (user.Senha != senha)
                        return RetornaSenhaInvalida();

                    db.Usuarios.Remove(user);
                    db.SaveChanges();

                    return RetornaOk();
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

        private static ValidationResult ValidaEntrada(Usuario usuario) => new UsuarioValidation().Validate(usuario);
             
        private Result<Usuario> RetornaNaoValido(ValidationResult valido)
        {
            result.Error = true;
            result.Message.AddRange(valido.Errors.Select(x => x.ErrorMessage));
            result.Status = HttpStatusCode.BadRequest;
            return result;
        }

        private Result<Usuario> RetornaUsuarioNaoEncontrado()
        {
            result.Error = true;
            result.Message.Add("Usuário não encontrado");
            result.Status = HttpStatusCode.NotFound;
            return result;
        }

        private Result<Usuario> RetornaSenhaInvalida()
        {
            result.Error = true;
            result.Message.Add("Senha inválida");
            result.Status = HttpStatusCode.BadRequest;
            return result;
        }

        private Result<Usuario> RetornaOk()
        {
            result.Data = user;
            result.Error = false;
            result.Message.Add("Ok");
            result.Status = HttpStatusCode.OK;
            return result;
        }
    }
}
