using HBSIS_Padawan.Sistema.Boletim.Models;
using HBSIS_Padawan.Sistema.Boletim.Models.Enums;
using HBSIS_Padawan.Sistema.Boletim.Validations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace HBSIS_Padawan.Sistema.Boletim.Tests
{
    [TestClass]
    public class CursoTest
    {
        [TestMethod]
        public void Testa_Curso_Ativo_True()
        {
            var curso = new Curso
            {
                Nome = "Teste Padawan",
                Situacao = Status.Ativo
            };

            var validations = new CursoValidation();
            var teste = validations.Validate(curso);

            Assert.IsTrue(teste.IsValid);
        }

        [TestMethod]
        public void Testa_Curso_Inativo_False()
        {
            var curso = new Curso
            {
                Nome = "Teste Padawan",
                Situacao = Status.Inativo
            };

            var validations = new CursoValidation();
            var teste = validations.Validate(curso);

            Assert.AreEqual("Inativo", curso.Situacao.ToString());
            Assert.IsFalse(teste.IsValid);
        }

        [TestMethod]
        public void Testa_Curso_Sem_Situação()
        {
            var curso = new Curso
            {
                Nome = "Teste Padawan"               
            };

            var validations = new CursoValidation();
            var teste = validations.Validate(curso);

            Assert.IsFalse(teste.IsValid);
        }

        [TestMethod]
        public void Testa_Curso_Nome_Empty()
        {
            var curso = new Curso
            {
                Nome = "",
                Situacao = Status.Ativo
            };

            var validations = new CursoValidation();
            var teste = validations.Validate(curso);

            Assert.IsFalse(teste.IsValid);
        }

        [TestMethod]
        public void Testa_Curso_Nome_Null()
        {
            var curso = new Curso
            {
                Nome = null,
                Situacao = Status.Ativo
            };

            var validations = new CursoValidation();
            var teste = validations.Validate(curso);

            Assert.IsFalse(teste.IsValid);
        }

        [TestMethod]
        public void Testa_Curso_Sem_Nome()
        {
            var curso = new Curso
            {                
                Situacao = Status.Ativo
            };

            var validations = new CursoValidation();
            var teste = validations.Validate(curso);

            Assert.IsFalse(teste.IsValid);
        }

        [TestMethod]
        public void Testa_Curso_Com_Números()
        {
            var curso = new Curso
            {
                Nome = "0123456789",
                Situacao = Status.Ativo
            };

            var validations = new CursoValidation();
            var teste = validations.Validate(curso);

            Assert.IsFalse(teste.IsValid);
        }

        [TestMethod]
        public void Testa_Curso_Com_Muitas_Letras()
        {
            var curso = new Curso
            {
                Nome = "TesteNomeDoCursoComMaisDeCinquentaCaracteresEmTeste",
                Situacao = Status.Ativo
            };

            var validations = new CursoValidation();
            var teste = validations.Validate(curso);

            Assert.IsFalse(teste.IsValid);
        }

        [TestMethod]
        public void Testa_Curso_Com_02_Letras()
        {
            var curso = new Curso
            {
                Nome = "Te",
                Situacao = Status.Ativo
            };

            var validations = new CursoValidation();
            var teste = validations.Validate(curso);

            Assert.IsFalse(teste.IsValid);
        }

        [TestMethod]
        public void Testa_Curso_Com_Materia_Ativa()
        {
            var materia = new Materia
            {
                Nome = "Teste materia",
                Cadastro = DateTime.Now,
                Descricao = "Teste descrição",
                Status = Status.Ativo
            };

            var curso = new Curso
            {
                Nome = "Teste Padawan",
                Situacao = Status.Ativo               
            };

            curso.CursoMaterias.Add(new CursoMateria
            {
                Curso = curso,
                Materia = materia
            });

            var validations = new CursoValidation();
            var teste = validations.Validate(curso);

            Assert.IsTrue(teste.IsValid);
        }

        [TestMethod]
        public void Testa_Curso_Com_Materia_Inativa()
        {
            var materia = new Materia
            {
                Nome = "Teste materia",
                Cadastro = DateTime.Now,
                Descricao = "Teste descrição",
                Status = Status.Inativo
            };

            var curso = new Curso
            {
                Nome = "Teste Padawan",
                Situacao = Status.Ativo
            };

            curso.CursoMaterias.Add(new CursoMateria
            {
                Curso = curso,
                Materia = materia
            });

            var validations = new CursoValidation();
            var teste = validations.Validate(curso);

            Assert.IsFalse(teste.IsValid);
        }
    }
}
