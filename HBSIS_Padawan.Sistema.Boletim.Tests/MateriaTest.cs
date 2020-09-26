using HBSIS_Padawan.Sistema.Boletim.Models;
using HBSIS_Padawan.Sistema.Boletim.Models.Enums;
using HBSIS_Padawan.Sistema.Boletim.Validations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace HBSIS_Padawan.Sistema.Boletim.Tests
{
    [TestClass]
    public class MateriaTest
    {
        [TestMethod]
        public void Testa_Materia_True()
        {
            var materia = new Materia
            {
                Nome = "Teste materia",
                Descricao = "Teste valido",
                Cadastro = new DateTime(2020, 01, 01),
                Status = Status.Inativo
            };

            var validation = new MateriaValidation();
            var teste = validation.Validate(materia);

            Assert.IsTrue(teste.IsValid);
        }

        [TestMethod]
        public void Testa_Status_Não_Ativo_Inativo()
        {
            var materia = new Materia
            {
                Nome = "Teste Status",
                Descricao = "Not Ativo/Inativo",
                Cadastro = new DateTime(2020, 01, 01),
                Status = Status.Concluido
            };

            var validation = new MateriaValidation();
            var teste = validation.Validate(materia);

            Assert.IsFalse(teste.IsValid);
        }

        [TestMethod]
        public void Testa_Status_em_String()
        {
            var materia = new Materia
            {
                Nome = "Teste convert.ToString",
                Descricao = "Usando Parse",
                Cadastro = new DateTime(2020, 01, 01),
                Status = Enum.Parse<Status>("Inativo")
            };

            var validation = new MateriaValidation();
            var teste = validation.Validate(materia);

            Assert.AreEqual("Inativo", materia.Status.ToString());
        }

        [TestMethod]
        public void Testa_Nome_Vazio()
        {
            var materia = new Materia
            {
                Nome = "",
                Descricao = "teste nome vazio",
                Cadastro = new DateTime(2020, 01, 01),
                Status = Status.Inativo
            };

            var validation = new MateriaValidation();
            var teste = validation.Validate(materia);

            Assert.IsFalse(teste.IsValid);
        }

        [TestMethod]
        public void Testa_Nome_Null()
        {
            var materia = new Materia
            {
                Nome = null,
                Descricao = "Teste nome null",
                Cadastro = new DateTime(2020, 01, 01),
                Status = Status.Ativo
            };

            var validation = new MateriaValidation();
            var teste = validation.Validate(materia);

            Assert.IsFalse(teste.IsValid);
        }

        [TestMethod]
        public void Testa_Descrição_Com_Numeros()
        {
            var materia = new Materia
            {
                Nome = "Teste Descrição com números",
                Descricao = "123",
                Cadastro = new DateTime(2020, 01, 01),
                Status = Status.Ativo
            };

            var validation = new MateriaValidation();
            var teste = validation.Validate(materia);

            Assert.IsFalse(teste.IsValid);
        }

        [TestMethod]
        public void Testa_Descrição_Com_Null()
        {
            var materia = new Materia
            {
                Nome = "Teste Descrição com null",
                Descricao = null,
                Cadastro = new DateTime(2020, 01, 01),
                Status = Status.Ativo
            };

            var validation = new MateriaValidation();
            var teste = validation.Validate(materia);

            Assert.IsFalse(teste.IsValid);
        }

        [TestMethod]
        public void Testa_Data_Futura()
        {
            var materia = new Materia
            {
                Nome = "Teste Data futura",
                Descricao = "Teste de volta para o futuro",
                Cadastro = new DateTime(2025, 01, 01),
                Status = Status.Ativo
            };

            var validation = new MateriaValidation();
            var teste = validation.Validate(materia);

            Assert.IsFalse(teste.IsValid);
        }

        [TestMethod]
        public void Testa_Usuario_Com_Mais_De_50_Caracteres()
        {
            var materia = new Materia
            {
                Nome = "TesteLoginDoUsuarioComMaisDeCinquentaCaracteresEmTeste",
                Descricao = "Teste de estouro de limite",
                Cadastro = new DateTime(2020, 01, 01),
                Status = Status.Ativo
            };

            var validation = new MateriaValidation();
            var teste = validation.Validate(materia);

            Assert.IsFalse(teste.IsValid);
        }
    }
}
