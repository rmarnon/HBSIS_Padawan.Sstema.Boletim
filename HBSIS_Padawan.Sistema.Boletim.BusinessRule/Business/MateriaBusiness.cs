using HBSIS_Padawan.Sistema.Boletim.BusinessRule.Exceptions;
using HBSIS_Padawan.Sistema.Boletim.BusinessRule.Interfaces;
using HBSIS_Padawan.Sistema.Boletim.Models;
using HBSIS_Padawan.Sistema.Boletim.Models.Enums;
using HBSIS_Padawan.Sistema.Boletim.Repositories.Data;
using HBSIS_Padawan.Sistema.Boletim.Util;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Net;

namespace HBSIS_Padawan.Sistema.Boletim.BusinessRule.Business
{
    public class MateriaBusiness : IMateria
    {
        private readonly ApplicationContext db;
        Result<Materia> result = new Result<Materia>();
        Materia materia = new Materia();

        public MateriaBusiness(ApplicationContext context) => db = context;

        public Result<Materia> Alteracoes(string nome, string novoNome, string descricao)
        {
            try
            {
                var valido = Retorno.ValidaEntrada(new Materia
                {
                    Nome = novoNome,
                    Descricao = descricao,
                    Cadastro = DateTime.Now,
                    Status = Status.Ativo
                });

                if (!valido.IsValid)
                    return Retorno.NãoValidaMateria(valido);

                using (db)
                {
                    materia = db.Materias.FirstOrDefault(x => x.Nome == nome);

                    if (materia is null)
                        return Retorno.NaoEncontradaMateria();

                    materia.Nome = novoNome;
                    materia.Descricao = descricao;

                    db.SaveChanges();

                    return Retorno.Ok(materia);
                }
            }
            catch (BusinessException e)
            {
                return RetornaErrosDesconhecidos(e);
            }
        }

        public Result<Materia> AlteraStatus(string nome, Status status)
        {
            try
            {
                using (db)
                {
                    materia = db.Materias.FirstOrDefault(x => x.Nome == nome);

                    if (materia is null)
                        return Retorno.NaoEncontradaMateria();

                    var valido = Retorno.ValidaEntrada(new Materia
                    {
                        Nome = nome,
                        Descricao = materia.Descricao,
                        Cadastro = materia.Cadastro,
                        Status = status
                    });

                    if (!valido.IsValid)
                        return Retorno.NãoValidaMateria(valido);

                    materia.Status = status;

                    db.SaveChanges();

                    return Retorno.Ok(materia);
                }
            }
            catch (BusinessException e)
            {
                return RetornaErrosDesconhecidos(e);
            }
        }

        public Result<Materia> Cadastrar(Materia materia)
        {
            try
            {
                var valido = Retorno.ValidaEntrada(materia);

                if (!valido.IsValid)
                    return Retorno.NãoValidaMateria(valido);

                using (db)
                {
                    foreach (var mat in db.Materias)
                    {
                        if (mat.Nome == materia.Nome)
                        {
                            result.Error = true;
                            result.Message.Add("Matéria já está cadastrada");
                            result.Status = HttpStatusCode.BadRequest;
                            return result;
                        }
                    }

                    db.Entry(materia).State = EntityState.Added;
                    db.SaveChanges();

                    return Retorno.Ok(materia);
                }
            }
            catch (BusinessException e)
            {
                return RetornaErrosDesconhecidos(e);
            }
        }

        public Result<Materia> Excluir(string nome)
        {
            try
            {
                using (db)
                {
                    materia = db.Materias.FirstOrDefault(x => x.Nome == nome);

                    if (materia is null)
                        return Retorno.NaoEncontradaMateria();

                    db.Materias.Remove(materia);
                    db.SaveChanges();

                    return Retorno.Ok(materia);
                }
            }
            catch (BusinessException e)
            {
                return RetornaErrosDesconhecidos(e);
            }
        }
                            
        private Result<Materia> RetornaErrosDesconhecidos(BusinessException e)
        {
            result.Error = true;
            result.Message.Add(e.Message);
            result.Status = HttpStatusCode.InternalServerError;
            return result;
        }
    }
}
