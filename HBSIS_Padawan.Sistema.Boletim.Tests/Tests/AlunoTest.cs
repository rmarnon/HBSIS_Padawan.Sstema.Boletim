﻿using HBSIS_Padawan.Sistema.Boletim.Models;
using HBSIS_Padawan.Sistema.Boletim.Models.Enums;
using HBSIS_Padawan.Sistema.Boletim.Validations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace HBSIS_Padawan.Sistema.Boletim.Tests
{
    [TestClass]
    public class AlunoTest
    {
        [TestMethod]
        public void Testa_Aluno_True()
        {
            var aluno = new Aluno
            {
                Nome = "Teste Nome",
                Sobrenome = "Teste Sobrenome",
                Nascimento = new DateTime(1982, 04, 29),
                Cpf = "01234567890"
            };

            var validation = new AlunoValidation();
            var teste = validation.Validate(aluno);

            Assert.IsTrue(teste.IsValid);
        }

        [TestMethod]
        public void Testa_Nome_Null()
        {
            var aluno = new Aluno
            {
                Nome = null,
                Sobrenome = "Teste Sobrenome",
                Nascimento = new DateTime(1982, 04, 29),
                Cpf = "01234567890"
            };

            var validation = new AlunoValidation();
            var teste = validation.Validate(aluno);

            Assert.IsFalse(teste.IsValid);
        }

        [TestMethod]
        public void Testa_Nome_Vazio()
        {
            var aluno = new Aluno
            {
                Nome = "",
                Sobrenome = "Teste Sobrenome",
                Nascimento = new DateTime(1982, 04, 29),
                Cpf = "01234567890"
            };

            var validation = new AlunoValidation();
            var teste = validation.Validate(aluno);

            Assert.IsFalse(teste.IsValid);
        }

        [TestMethod]
        public void Testa_Nome_Com_Numeros()
        {
            var aluno = new Aluno
            {
                Nome = "999999999",
                Sobrenome = "Teste Sobrenome",
                Nascimento = new DateTime(1982, 04, 29),
                Cpf = "01234567890"
            };

            var validation = new AlunoValidation();
            var teste = validation.Validate(aluno);

            Assert.IsFalse(teste.IsValid);
        }

        [TestMethod]
        public void Testa_Nome_Com_02_Letras()
        {
            var aluno = new Aluno
            {
                Nome = "Te",
                Sobrenome = "Teste Sobrenome",
                Nascimento = new DateTime(1982, 04, 29),
                Cpf = "01234567890"
            };

            var validation = new AlunoValidation();
            var teste = validation.Validate(aluno);

            Assert.IsFalse(teste.IsValid);
        }

        [TestMethod]
        public void Testa_Nome_Com_Muitas_Letras()
        {
            var aluno = new Aluno
            {
                Nome = "TestNomeComMaisDeVinteLetras",
                Sobrenome = "Teste Sobrenome",
                Nascimento = new DateTime(1982, 04, 29),
                Cpf = "01234567890"
            };

            var validation = new AlunoValidation();
            var teste = validation.Validate(aluno);

            Assert.IsFalse(teste.IsValid);
        }

        [TestMethod]
        public void Testa_Sobrenome_Com_Muitas_Letras()
        {
            var aluno = new Aluno
            {
                Nome = "Teste",
                Sobrenome = "TestSobrenomeComMaisDeVinteLetras",
                Nascimento = new DateTime(1982, 04, 29),
                Cpf = "01234567890"
            };

            var validation = new AlunoValidation();
            var teste = validation.Validate(aluno);

            Assert.IsFalse(teste.IsValid);
        }

        [TestMethod]
        public void Testa_Nome_Não_Informado()
        {
            var aluno = new Aluno
            {                
                Sobrenome = "Teste Sobrenome",
                Nascimento = new DateTime(1982, 04, 29),
                Cpf = "01234567890"
            };

            var validation = new AlunoValidation();
            var teste = validation.Validate(aluno);

            Assert.IsFalse(teste.IsValid);
        }

        [TestMethod]
        public void Testa_Sobrenomeome_Não_Informado()
        {
            var aluno = new Aluno
            {
                Nome = "Teste sobrenome",                
                Nascimento = new DateTime(1982, 04, 29),
                Cpf = "01234567890"
            };

            var validation = new AlunoValidation();
            var teste = validation.Validate(aluno);

            Assert.IsFalse(teste.IsValid);
        }

        [TestMethod]
        public void Testa_Nascimento_Não_Informado()
        {
            var aluno = new Aluno
            {
                Nome = "Teste Nome",
                Sobrenome = "Teste Sobrenome",                
                Cpf = "01234567890"
            };

            var validation = new AlunoValidation();
            var teste = validation.Validate(aluno);

            Assert.IsFalse(teste.IsValid);
        }

        [TestMethod]
        public void Testa_Nascimento_Maior_01_01_2002()
        {
            var aluno = new Aluno
            {
                Nome = "Teste nome",
                Sobrenome = "Teste Sobrenome",
                Nascimento = new DateTime(2002, 01, 02),
                Cpf = "01234567890"
            };

            var validation = new AlunoValidation();
            var teste = validation.Validate(aluno);

            Assert.IsFalse(teste.IsValid);
        }

        [TestMethod]
        public void Testa_CPF_Com_Letras()
        {
            var aluno = new Aluno
            {
                Nome = "Teste nome",
                Sobrenome = "Teste Sobrenome",
                Nascimento = new DateTime(2000, 01, 01),
                Cpf = "A5477C13187"
            };

            var validation = new AlunoValidation();
            var teste = validation.Validate(aluno);

            Assert.IsFalse(teste.IsValid);
        }

        [TestMethod]
        public void Testa_CPF_Com_Muitos_Numeros()
        {
            var aluno = new Aluno
            {
                Nome = "Teste nome",
                Sobrenome = "Teste Sobrenome",
                Nascimento = new DateTime(2000, 01, 01),
                Cpf = "1234567891011"
            };

            var validation = new AlunoValidation();
            var teste = validation.Validate(aluno);

            Assert.IsFalse(teste.IsValid);
        }

        [TestMethod]
        public void Testa_CPF_Com_Menos_Numeros()
        {
            var aluno = new Aluno
            {
                Nome = "Teste nome",
                Sobrenome = "Teste Sobrenome",
                Nascimento = new DateTime(2000, 01, 01),
                Cpf = "012345"
            };

            var validation = new AlunoValidation();
            var teste = validation.Validate(aluno);

            Assert.IsFalse(teste.IsValid);
        }

        [TestMethod]
        public void Testa_CPF_vazio()
        {
            var aluno = new Aluno
            {
                Nome = "Teste nome",
                Sobrenome = "Teste Sobrenome",
                Nascimento = new DateTime(2000, 01, 01),
                Cpf = ""
            };

            var validation = new AlunoValidation();
            var teste = validation.Validate(aluno);

            Assert.IsFalse(teste.IsValid);
        }

        [TestMethod]
        public void Testa_CPF_Null()
        {
            var aluno = new Aluno
            {
                Nome = "Teste nome",
                Sobrenome = "Teste Sobrenome",
                Nascimento = new DateTime(2000, 01, 01),
                Cpf = null
            };

            var validation = new AlunoValidation();
            var teste = validation.Validate(aluno);

            Assert.IsFalse(teste.IsValid);
        }

        [TestMethod]
        public void Testa_CPF_Não_Informado()
        {
            var aluno = new Aluno
            {
                Nome = "Teste nome",
                Sobrenome = "Teste Sobrenome",
                Nascimento = new DateTime(2000, 01, 01)                
            };

            var validation = new AlunoValidation();
            var teste = validation.Validate(aluno);

            Assert.IsFalse(teste.IsValid);
        }

        [TestMethod]
        public void Testa_CPF_No_Formato_Padrao()
        {
            var aluno = new Aluno
            {
                Nome = "Teste nome",
                Sobrenome = "Teste Sobrenome",
                Nascimento = new DateTime(2000, 01, 01),
                Cpf = "012.345.678-90"
            };

            var validation = new AlunoValidation();
            var teste = validation.Validate(aluno);

            Assert.IsTrue(teste.IsValid);
        }

        [TestMethod]
        public void Testa_CPF_No_Formato_Invalido()
        {
            var aluno = new Aluno
            {
                Nome = "Teste nome",
                Sobrenome = "Teste Sobrenome",
                Nascimento = new DateTime(2000, 01, 01),
                Cpf = "054*773 136/17"
            };

            var validation = new AlunoValidation();
            var teste = validation.Validate(aluno);

            Assert.IsFalse(teste.IsValid);
        }

        [TestMethod]
        public void Testa_Matricula_Curso_Ativo()
        {
            var aluno = new Aluno
            {
                Nome = "Teste nome",
                Sobrenome = "Teste Sobrenome",
                Nascimento = new DateTime(2000, 01, 01),
                Cpf = "01234567890"
            };

            var curso = new Curso
            {
                Nome = "Teste matricula",
                Situacao = Status.Ativo
            };

            aluno.AlunoCursos.Add(new AlunoCurso
            {
                Aluno = aluno,
                Curso = curso
            });

            var validation = new AlunoValidation();
            var teste = validation.Validate(aluno);

            Assert.IsTrue(teste.IsValid);
        }

        [TestMethod]
        public void Testa_Matricula_Curso_Inativo()
        {
            var aluno = new Aluno
            {
                Nome = "Teste nome",
                Sobrenome = "Teste Sobrenome",
                Nascimento = new DateTime(2000, 01, 01),
                Cpf = "01234567890"
            };

            var curso = new Curso
            {
                Nome = "Teste matricula",
                Situacao = Status.Inativo
            };

            aluno.AlunoCursos.Add(new AlunoCurso
            {
                Aluno = aluno,
                Curso = curso
            });

            var validation = new AlunoValidation();
            var teste = validation.Validate(aluno);

            Assert.IsFalse(teste.IsValid);
        }

        [TestMethod]
        public void Testa_Inserir_Nota_Maxima()
        {
            var aluno = new Aluno
            {
                Nome = "Teste nome",
                Sobrenome = "Teste Sobrenome",
                Nascimento = new DateTime(2000, 01, 01),
                Cpf = "01234567890"
            };

            var materia = new Materia
            {
                Nome = "Teste materia",
                Descricao = "Teste descricao",
                Status = Status.Ativo,
                Cadastro = new DateTime(2020, 06, 07)
            };

            aluno.AlunoMaterias.Add(new AlunoMateria 
            {
                Aluno = aluno,
                Materia = materia,
                Nota = 100.0
            });

            var validation = new AlunoValidation();
            var teste = validation.Validate(aluno);

            Assert.IsTrue(teste.IsValid);
        }

        [TestMethod]
        public void Testa_Inserir_Nota_Zero()
        {
            var aluno = new Aluno
            {
                Nome = "Teste nome",
                Sobrenome = "Teste Sobrenome",
                Nascimento = new DateTime(2000, 01, 01),
                Cpf = "01234567890"
            };

            var materia = new Materia
            {
                Nome = "Teste materia",
                Descricao = "Teste descricao",
                Status = Status.Ativo,
                Cadastro = new DateTime(2020, 06, 07)
            };

            aluno.AlunoMaterias.Add(new AlunoMateria
            {
                Aluno = aluno,
                Materia = materia,
                Nota = 0.0
            });

            var validation = new AlunoValidation();
            var teste = validation.Validate(aluno);

            Assert.IsTrue(teste.IsValid);
        }

        [TestMethod]
        public void Testa_Inserir_Nota_Negativa()
        {
            var aluno = new Aluno
            {
                Nome = "Teste nome",
                Sobrenome = "Teste Sobrenome",
                Nascimento = new DateTime(2000, 01, 01),
                Cpf = "01234567890"
            };

            var materia = new Materia
            {
                Nome = "Teste materia",
                Descricao = "Teste descricao",
                Status = Status.Ativo,
                Cadastro = new DateTime(2020, 06, 07)
            };

            aluno.AlunoMaterias.Add(new AlunoMateria
            {
                Aluno = aluno,
                Materia = materia,
                Nota = - 1.0
            });

            var validation = new AlunoValidation();
            var teste = validation.Validate(aluno);

            Assert.IsFalse(teste.IsValid);
        }

        [TestMethod]
        public void Testa_Inserir_Nota_Maior_Que_100()
        {
            var aluno = new Aluno
            {
                Nome = "Teste nome",
                Sobrenome = "Teste Sobrenome",
                Nascimento = new DateTime(2000, 01, 01),
                Cpf = "01234567890"
            };

            var materia = new Materia
            {
                Nome = "Teste materia",
                Descricao = "Teste descricao",
                Status = Status.Ativo,
                Cadastro = new DateTime(2020, 06, 07)
            };

            aluno.AlunoMaterias.Add(new AlunoMateria
            {
                Aluno = aluno,
                Materia = materia,
                Nota = 101.0
            });

            var validation = new AlunoValidation();
            var teste = validation.Validate(aluno);

            Assert.IsFalse(teste.IsValid);
        }

        [TestMethod]
        public void Testa_Nota_Não_Informada_Padrao_Zero()
        {
            var aluno = new Aluno
            {
                Nome = "Teste nome",
                Sobrenome = "Teste Sobrenome",
                Nascimento = new DateTime(2000, 01, 01),
                Cpf = "01234567890"
            };

            var materia = new Materia
            {
                Nome = "Teste materia",
                Descricao = "Teste descricao",
                Status = Status.Ativo,
                Cadastro = new DateTime(2020, 06, 07)
            };

            aluno.AlunoMaterias.Add(new AlunoMateria
            {
                Aluno = aluno,
                Materia = materia                
            });

            var validation = new AlunoValidation();
            var teste = validation.Validate(aluno);
                       
            Assert.IsTrue(teste.IsValid);
        }
    }
}
