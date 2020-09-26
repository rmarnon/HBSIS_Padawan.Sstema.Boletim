using HBSIS_Padawan.Sistema.Boletim.Models;
using HBSIS_Padawan.Sistema.Boletim.Validations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HBSIS_Padawan.Sistema.Boletim.Tests
{
    [TestClass]
    public class UsuarioTest
    {
        [TestMethod]
        public void Testa_Usuario_True()
        {
            Usuario user = new Usuario
            {
                Login = "Rodrigo",
                Senha = "12345678910"
            };

            var validation = new UsuarioValidation();
            var teste = validation.Validate(user);

            Assert.IsTrue(teste.IsValid);
        }

        [TestMethod]
        public void Testa_Login_Empty()
        {
            Usuario user = new Usuario
            {
                Login = "",
                Senha = "12345678910"
            };

            var validation = new UsuarioValidation();
            var teste = validation.Validate(user);

            Assert.IsFalse(teste.IsValid);
        }

        [TestMethod]
        public void Testa_Senha_Tres_Caracteres()
        {
            Usuario user = new Usuario
            {
                Login = "Rodrigo",
                Senha = "123"
            };

            var validation = new UsuarioValidation();
            var teste = validation.Validate(user);

            Assert.IsFalse(teste.IsValid);
        }

        [TestMethod]
        public void Testa_Login_Null()
        {
            Usuario user = new Usuario
            {
                Login = null,
                Senha = "123"
            };

            var validation = new UsuarioValidation();
            var teste = validation.Validate(user);

            Assert.IsFalse(teste.IsValid);
        }

        [TestMethod]
        public void Testa_Login_Mais50_Letras()
        {
            Usuario user = new Usuario
            {
                Login = "asdgdsfijorjoisadhashasiodhasodiahsdoiashoiashdoiashdao",
                Senha = "12345678900"
            };

            var validation = new UsuarioValidation();
            var teste = validation.Validate(user);

            Assert.IsFalse(teste.IsValid);
        }
    }
}
