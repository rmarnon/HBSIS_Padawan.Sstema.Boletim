using FluentValidation.Results;
using HBSIS_Padawan.Sistema.Boletim.Models;
using HBSIS_Padawan.Sistema.Boletim.Validations;
using System.Linq;
using System.Net;

namespace HBSIS_Padawan.Sistema.Boletim.Util
{
    public static class Retorno
    {
        public static ValidationResult ValidaEntrada(Usuario usuario) => new UsuarioValidation().Validate(usuario);
        public static ValidationResult ValidaEntrada(Materia materia) => new MateriaValidation().Validate(materia);
        public static ValidationResult ValidaEntrada(Aluno aluno) => new AlunoValidation().Validate(aluno);
        public static ValidationResult ValidaEntrada(Curso curso) => new CursoValidation().Validate(curso);

        public static Result<Aluno> Ok(Aluno aluno)
        {
            Result<Aluno> result = new Result<Aluno>();
            result.Data = aluno;
            result.Error = false;
            result.Message.Add("Ok");
            result.Status = HttpStatusCode.OK;
            return result;
        }

        public static  Result<Curso> Ok(Curso curso)
        {
            Result<Curso> result = new Result<Curso>();
            result.Data = curso;
            result.Error = false;
            result.Message.Add("Ok");
            result.Status = HttpStatusCode.OK;
            return result;
        }

        public static Result<Materia> Ok(Materia materia)
        {
            Result<Materia> result = new Result<Materia>();
            result.Data = materia;
            result.Error = false;
            result.Message.Add("Ok");
            result.Status = HttpStatusCode.OK;
            return result;
        }

        public static Result<Usuario> Ok(Usuario user)
        {
            Result<Usuario> result = new Result<Usuario>();
            result.Data = user;
            result.Error = false;
            result.Message.Add("Ok");
            result.Status = HttpStatusCode.OK;
            return result;
        }

        public static Result<Aluno> NaoEncontradoAluno()
        {
            Result<Aluno> result = new Result<Aluno>();
            result.Error = true;
            result.Message.Add("Aluno não está matriculado");
            result.Status = HttpStatusCode.NotFound;
            return result;
        }

        public static  Result<Curso> NaoEncontradoCurso()
        {
            Result<Curso> result = new Result<Curso>();
            result.Error = true;
            result.Message.Add("Curso não está cadastrado");
            result.Status = HttpStatusCode.NotFound;
            return result;
        }

        public static Result<Materia> NaoEncontradaMateria()
        {
            Result<Materia> result = new Result<Materia>();
            result.Error = true;
            result.Message.Add("Matéria não está cadastrada");
            result.Status = HttpStatusCode.NotFound;
            return result;
        }

        public static Result<Usuario> NaoEncontradoUsuario()
        {
            Result<Usuario> result = new Result<Usuario>();
            result.Error = true;
            result.Message.Add("Usuário não encontrado");
            result.Status = HttpStatusCode.NotFound;
            return result;
        }

        public static  Result<Aluno> NaoValidaAluno(ValidationResult valido)
        {
            Result<Aluno> result = new Result<Aluno>();
            result.Error = true;
            result.Message.AddRange(valido.Errors.Select(x => x.ErrorMessage));
            result.Status = HttpStatusCode.BadRequest;
            return result;
        }

        public static Result<Curso> NaoValidaCurso(ValidationResult valido)
        {
            Result<Curso> result = new Result<Curso>();
            result.Error = true;
            result.Message.AddRange(valido.Errors.Select(x => x.ErrorMessage));
            result.Status = HttpStatusCode.BadRequest;
            return result;
        }

        public static Result<Materia> NãoValidaMateria(ValidationResult valido)
        {
            Result<Materia> result = new Result<Materia>();
            result.Error = true;
            result.Message.AddRange(valido.Errors.Select(x => x.ErrorMessage));
            result.Status = HttpStatusCode.BadRequest;
            return result;
        }

        public static Result<Usuario> NaoValidaUsuario(ValidationResult valido)
        {
            Result<Usuario> result = new Result<Usuario>();
            result.Error = true;
            result.Message.AddRange(valido.Errors.Select(x => x.ErrorMessage));
            result.Status = HttpStatusCode.BadRequest;
            return result;
        }

        public static Result<Usuario> SenhaInvalida()
        {
            Result<Usuario> result = new Result<Usuario>();
            result.Error = true;
            result.Message.Add("Senha inválida");
            result.Status = HttpStatusCode.BadRequest;
            return result;
        }
    }
}
