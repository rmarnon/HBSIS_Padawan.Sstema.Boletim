using System;

namespace HBSIS_Padawan.Sistema.Boletim.Validations.Rules
{
    public static class CadastroValidation
    {
        public static bool Validate(DateTime cadastro) => cadastro < DateTime.Now;
    }
}
