using HBSIS_Padawan.Sistema.Boletim.BusinessRule.Exceptions;
using HBSIS_Padawan.Sistema.Boletim.BusinessRule.Interfaces;
using HBSIS_Padawan.Sistema.Boletim.Models;
using HBSIS_Padawan.Sistema.Boletim.Models.Enums;
using HBSIS_Padawan.Sistema.Boletim.Repositories.Data;
using HBSIS_Padawan.Sistema.Boletim.Util;
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
                var valido = Retorno.ValidaEntrada(curso);

                if (!valido.IsValid)
                    return Retorno.NaoValidaCurso(valido);

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

                    return Retorno.Ok(curso);
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
                        return Retorno.NaoEncontradoCurso();

                    curso.Situacao = situacao;
                    db.SaveChanges();

                    return Retorno.Ok(curso);
                }
            }
            catch (BusinessException e)
            {
                return RetornaErroDesconhecido(e);
            }
        }

        public Result<Curso> VinculaMateria(string nomeCurso, string nomeMateria)
        {
            try
            {
                using (db)
                {
                    curso = db.Cursos.FirstOrDefault(x => x.Nome == nomeCurso);

                    if (curso is null)
                        return Retorno.NaoEncontradoCurso();

                    var validaCurso = Retorno.ValidaEntrada(curso);

                    if (!validaCurso.IsValid)
                        return Retorno.NaoValidaCurso(validaCurso);

                    materia = db.Materias.FirstOrDefault(y => y.Nome == nomeMateria);

                    if (materia is null)
                    {
                        result.Error = true;
                        result.Message.Add("Materia não cadastrada");
                        result.Status = HttpStatusCode.NotFound;
                        return result;
                    }

                    var validaMateria = Retorno.ValidaEntrada(materia);

                    if (!validaMateria.IsValid)
                        return Retorno.NaoValidaCurso(validaMateria);

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

                    return Retorno.Ok(curso);

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
                        return Retorno.NaoEncontradoCurso();

                    db.Cursos.Remove(curso);
                    db.SaveChanges();

                    return Retorno.Ok(curso);
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
    }
}
