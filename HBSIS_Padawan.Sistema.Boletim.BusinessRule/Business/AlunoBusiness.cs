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
    public class AlunoBusiness : IStudent
    {
        private readonly ApplicationContext db;

        public AlunoBusiness(ApplicationContext context) => db = context;
        Result<Aluno> result = new Result<Aluno>();
        Aluno aluno = new Aluno();
        Curso curso = new Curso();
        Materia materia = new Materia();

        public Result<Aluno> Matricular(Aluno aluno, string nomeCurso)
        {
            try
            {
                var valido = Retorno.ValidaEntrada(aluno);

                if (!valido.IsValid)                
                    return Retorno.NaoValidaAluno(valido);

                using (db)
                {
                    foreach (var student in db.Alunos)
                    {
                        if (student.Cpf == aluno.Cpf)
                        {
                            result.Error = true;
                            result.Message.Add("Aluno já está cadastrado");
                            result.Status = HttpStatusCode.BadRequest;
                            return result;
                        }
                    }

                    curso = db.Cursos.FirstOrDefault(x => x.Nome == nomeCurso);

                    if (curso is null)
                    {
                        result.Error = true;
                        result.Message.Add("Curso não está sendo ofertado");
                        result.Status = HttpStatusCode.NotFound;
                        return result;
                    }

                    if (curso.Situacao != Status.Ativo)
                    {
                        result.Error = true;
                        result.Message.Add("A matrícula só é permitida para cursos com status 'Ativo'");
                        result.Status = HttpStatusCode.BadRequest;
                        return result;
                    }

                    var alunoCurso = new AlunoCurso { Aluno = aluno, Curso = curso };
                                        
                    db.Entry(aluno).State = EntityState.Added;
                    db.Entry(alunoCurso).State = EntityState.Added;
                    db.SaveChanges();

                    return Retorno.Ok(aluno);
                }
            }
            catch (BusinessException e)
            {
                return RetornaErroDesconhecido(e);
            }
        }

        public Result<Aluno> InserirNota(string cpf, string nomeMateria, double nota)
        {
            try
            {
                using (db)
                {
                    materia = db.Materias.FirstOrDefault(x => x.Nome == nomeMateria);

                    if (materia is null)
                    {
                        result.Error = true;
                        result.Message.Add("Matéria não cadastrada");
                        result.Status = HttpStatusCode.NotFound;
                        return result;
                    }

                    var valido = Retorno.ValidaEntrada(materia);

                    if (!valido.IsValid)
                    {
                        result.Error = true;
                        result.Message.AddRange(valido.Errors.Select(x => x.ErrorMessage));
                        result.Status = HttpStatusCode.BadRequest;
                        return result;
                    }

                    if (materia.Status != Status.Ativo)
                    {
                        result.Error = true;
                        result.Message.Add("Materia deve estar com status 'Ativo' para receber notas");
                        result.Status = HttpStatusCode.BadRequest;
                        return result;
                    }                        

                    aluno = db.Alunos.FirstOrDefault(x => x.Cpf == cpf);

                    if (aluno is null)
                        return Retorno.NaoEncontradoAluno();

                    aluno.AlunoMaterias.Add(
                        new AlunoMateria 
                        {
                            Aluno = aluno, 
                            Materia = materia, 
                            Nota = nota
                        });

                    valido = Retorno.ValidaEntrada(aluno);

                    if (!valido.IsValid)
                        return Retorno.NaoValidaAluno(valido);

                    db.SaveChanges();

                    return Retorno.Ok(aluno);                        
                }
            }
            catch (BusinessException e)
            {
                return RetornaErroDesconhecido(e);
            }
        }

        public Result<Aluno> AlterarNome(string cpf, string novoNome)
        {
            try
            {
                using (db)
                {
                    aluno = db.Alunos.FirstOrDefault(x => x.Cpf == cpf);

                    if (aluno is null)                  
                        return Retorno.NaoEncontradoAluno();                  

                    aluno.Nome = novoNome;

                    var valido = Retorno.ValidaEntrada(aluno);

                    if (!valido.IsValid)
                        return Retorno.NaoValidaAluno(valido);
                                       
                    db.SaveChanges();

                    return Retorno.Ok(aluno);
                }
            }
            catch (BusinessException e)
            {
                return RetornaErroDesconhecido(e);
            }
        }

        public Result<Aluno> AlterarSobrenome(string cpf, string novoSobrenome)
        {
            try
            {
                using (db)
                {
                    aluno = db.Alunos.FirstOrDefault(x => x.Cpf == cpf);

                    if (aluno is null)                   
                        return Retorno.NaoEncontradoAluno();                   

                    aluno.Sobrenome = novoSobrenome;

                    var valido = Retorno.ValidaEntrada(aluno);

                    if (!valido.IsValid)
                       return  Retorno.NaoValidaAluno(valido);

                    db.SaveChanges();

                    return Retorno.Ok(aluno);
                }
            }
            catch (BusinessException e)
            {
                return RetornaErroDesconhecido(e);
            }
        }

        public Result<Aluno> Excluir(string cpf)
        {
            try
            {
                using (db)
                {
                    aluno = db.Alunos.FirstOrDefault(x => x.Cpf == cpf);

                    if (aluno is null)
                        return Retorno.NaoEncontradoAluno();

                    db.Alunos.Remove(aluno);
                    db.SaveChanges();

                    return Retorno.Ok(aluno);
                }
            }
            catch (BusinessException e)
            {
                return RetornaErroDesconhecido(e);
            }
        } 
 
        private Result<Aluno> RetornaErroDesconhecido(BusinessException e)
        {
            result.Error = true;
            result.Message.Add(e.Message);
            result.Status = HttpStatusCode.InternalServerError;
            return result;
        }
    }
}
