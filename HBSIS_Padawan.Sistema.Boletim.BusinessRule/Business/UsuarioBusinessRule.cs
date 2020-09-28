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

        public UsuarioBusinessRule(ApplicationContext context) => db = context;

        public Result<Usuario> AlterarLogin(string login, string novoLogin, string senha)
        {
            try
            {
                var validation = new UsuarioValidation();
                var valido = validation.Validate(new Usuario { Login = login, Senha = senha });

                using (db)
                {
                    var user = db.Usuarios.FirstOrDefault(x => x.Login == login && x.Senha == senha);

                    if (!valido.IsValid || user is null)
                    {
                        result.Error = true;
                        result.Message.Add(valido.Errors.ToString());
                        result.Status = HttpStatusCode.BadRequest;
                        return result;
                    }

                    user.Login = novoLogin;                    
                    db.SaveChanges();

                    result.Data = user;
                    result.Error = false;
                    result.Message.Add("Login Alterado");
                    result.Status = HttpStatusCode.OK;
                    return result;

                }
            }
            catch (BusinessException e)
            {
                return ReturnErrors(e);
            }
        }

        public Result<Usuario> AlteraSenha(string login, string senha, string novaSenha)
        {
            try
            {
                var validation = new UsuarioValidation();
                var valido = validation.Validate(new Usuario { Login = login, Senha = senha });

                using (db)
                {
                    Usuario user = db.Usuarios.FirstOrDefault(x => x.Login == login && x.Senha == senha);

                    if (!valido.IsValid || user is null)
                    {
                        result.Error = true;
                        result.Message.Add(valido.Errors.ToString());
                        result.Status = HttpStatusCode.BadRequest;
                        return result;
                    }

                    user.Senha = novaSenha;
                    db.SaveChanges();

                    result.Data = user;
                    result.Error = false;
                    result.Message.Add("Senha Alterado");
                    result.Status = HttpStatusCode.OK;
                    return result;

                }
            }
            catch (BusinessException e) 
            {
                return ReturnErrors(e);
            }
        }

        public Result<Usuario> Cadastrar(string login, string senha, TipoUsuario tipo)
        {
            try
            {
                Usuario user = new Usuario
                {
                    Login = login,
                    Senha = senha,
                    Tipo = tipo
                };

                var validation = new UsuarioValidation();
                var valido = validation.Validate(user);

                if (!valido.IsValid)
                {
                    result.Error = true;
                    result.Message.Add(valido.Errors.ToString());
                    result.Status = HttpStatusCode.BadRequest;
                    return result;
                }

                using (db)
                {
                    foreach (var usuario in db.Usuarios)
                    {
                        if (usuario.Login == login)
                        {
                            result.Error = true;
                            result.Message.Add("Usuário já esta cadastrado");
                            result.Status = HttpStatusCode.BadRequest;
                            return result;
                        }
                    }

                    db.Usuarios.Add(user);
                    db.SaveChanges();

                    result.Error = false;
                    result.Message.Add("Ok");
                    result.Status = HttpStatusCode.OK;
                    result.Data = user;
                    return result;
                }
            }
            catch (BusinessException e) 
            {
                return ReturnErrors(e);
            }
        }

        public Result<Usuario> Conectar(string login, string senha)
        {
            try
            {
                using (db)
                {
                    Usuario user = db.Usuarios.FirstOrDefault(x => x.Login == login && x.Senha == senha);

                    if (user is null)
                    {
                        result.Error = true;
                        result.Message.Add("Usuário não cadastrado");
                        result.Status = HttpStatusCode.NotFound;
                        return result;
                    }
                    
                    result.Error = false;
                    result.Message.Add("Ok");
                    result.Status = HttpStatusCode.OK;
                    result.Data = user;
                    return result;
                }
            }
            catch (BusinessException e)
            {
                return ReturnErrors(e);
            }
        }

        public Result<Usuario> Excluir(string login, string senha)
        {
            try
            {
                using (db)
                {
                    Usuario user = db.Usuarios.FirstOrDefault(x => x.Login == login && x.Senha == senha);

                    if (user is null)
                    {
                        result.Error = true;
                        result.Message.Add("Usuário não cadastrado");
                        result.Status = HttpStatusCode.NotFound;
                        return result;
                    }

                    db.Usuarios.Remove(user);
                    db.SaveChanges();

                    result.Error = false;
                    result.Message.Add("Usuário removido com sucesso");
                    result.Status = HttpStatusCode.OK;

                    return result;
                }
            }
            catch (BusinessException e)
            {
                return ReturnErrors(e);
            }            
        }

        private Result<Usuario> ReturnErrors(BusinessException e)
        {
            result.Error = true;
            result.Message.Add(e.Message);
            result.Status = HttpStatusCode.InternalServerError;
            return result;
        }
    }
}
