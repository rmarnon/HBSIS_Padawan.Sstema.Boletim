using FluentValidation.Results;
using HBSIS_Padawan.Sistema.Boletim.BusinessRule.Exceptions;
using HBSIS_Padawan.Sistema.Boletim.BusinessRule.Interfaces;
using HBSIS_Padawan.Sistema.Boletim.Models;
using HBSIS_Padawan.Sistema.Boletim.Models.Enums;
using HBSIS_Padawan.Sistema.Boletim.Repositories.Data;
using HBSIS_Padawan.Sistema.Boletim.Util;
using HBSIS_Padawan.Sistema.Boletim.Validations;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net;

namespace HBSIS_Padawan.Sistema.Boletim.BusinessRule.Business
{
    public class CursoBusiness : ICourse
    {
        private readonly ApplicationContext db;

        public CursoBusiness(ApplicationContext context) => db = context;
        Result<Curso> result = new Result<Curso>();
        Curso curso = new Curso();
        Materia materia = new Materia();

        public Result<Curso> Cadastar(Curso curso)
        {
            try
            {
                var valido = ValidaEntrada(curso);

                if (!valido.IsValid)
                    return RetornaNaoValido(valido);

                using (db)
                {
                    foreach (var course in db.Cursos)
                    {
                        if (course.Nome == curso.Nome)
                        {
                            result.Error = true;
                            result.Message.Add("Curso já está cadastrado");
                            result.Status = HttpStatusCode.BadRequest;
                            return result;
                        }
                    }

                    db.Entry(curso).State = EntityState.Added;
                    db.SaveChanges();

                    return RetornaOk(curso);
                }
            }
            catch (BusinessException e)
            {
                return RetornaErroDesconhecido(e);
            }
        }

        public Result<Curso> AlteraSituacao(string nome, Status situacao)
        {
            try
            {
                using (db)
                {
                    curso = db.Cursos.FirstOrDefault(x => x.Nome == nome);

                    if (curso is null)
                        return RetornaNaoEncontrado();

                    curso.Situacao = situacao;
                    db.SaveChanges();

                    return RetornaOk(curso);
                }
            }
            catch (BusinessException e)
            {
                return RetornaErroDesconhecido(e);
            }
        }

        public Result<Curso> CadastraMateria(string nomeCurso, string nomeMateria)
        {
            try
            {
                using (db)
                {
                    curso = db.Cursos.FirstOrDefault(x => x.Nome == nomeCurso);                    

                    if (curso is null)
                        return RetornaNaoEncontrado();

                    var validaCurso = ValidaEntrada(curso);

                    if (!validaCurso.IsValid)
                        return RetornaNaoValido(validaCurso);

                    materia = db.Materias.FirstOrDefault(y => y.Nome == nomeMateria);

                    if (materia is null)
                        return RetornaNaoEncontrado();

                    var validaMateria = ValidaEntrada(materia);                    

                    if (!validaMateria.IsValid)
                        return RetornaNaoValido(validaMateria);

                    if (materia.Status != Status.Ativo)
                    {
                        result.Error = true;
                        result.Message.Add("Curso só permite cadastro de matérias com status 'Ativo'");
                        result.Status = HttpStatusCode.BadRequest;
                        return result;
                    }

                    var cursoMateria = new CursoMateria { Curso = curso, Materia = materia };

                    db.Entry(cursoMateria).State = EntityState.Added;
                    db.SaveChanges();

                    return RetornaOk(curso);

                }                  
            }
            catch (BusinessException e)
            {
                return RetornaErroDesconhecido(e);
            }
        }

        public Result<Curso> Excluir(string nome)
        {
            try
            {
                using (db)
                {
                    curso = db.Cursos.FirstOrDefault(x => x.Nome == nome);

                    if (curso is null)
                        return RetornaNaoEncontrado();

                    db.Cursos.Remove(curso);
                    db.SaveChanges();
                    
                    return RetornaOk(curso);
                }
            }
            catch (BusinessException e)
            {
                return RetornaErroDesconhecido(e);
            }
        }

        private Result<Curso> RetornaErroDesconhecido(BusinessException e)
        {
            result.Error = true;
            result.Message.Add(e.Message);
            result.Status = HttpStatusCode.InternalServerError;
            return result;
        }

        private Result<Curso> RetornaOk(Curso curso)
        {
            result.Data = curso;
            result.Error = false;
            result.Message.Add("Ok");
            result.Status = HttpStatusCode.OK;
            return result;
        }

        private Result<Curso> RetornaNaoEncontrado()
        {
            result.Error = true;
            result.Message.Add("'Curso' ou 'Matéria' não está cadastrado");
            result.Status = HttpStatusCode.NotFound;
            return result;
        }

        private Result<Curso> RetornaNaoValido(ValidationResult valido)
        {
            result.Error = true;
            result.Message.AddRange(valido.Errors.Select(x => x.ErrorMessage));
            result.Status = HttpStatusCode.BadRequest;
            return result;
        }

        private static ValidationResult ValidaEntrada(Curso curso) => new CursoValidation().Validate(curso);

        private static ValidationResult ValidaEntrada(Materia materia) => new MateriaValidation().Validate(materia);
    }
}
