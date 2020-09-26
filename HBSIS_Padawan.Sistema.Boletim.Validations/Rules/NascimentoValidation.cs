using System;

namespace HBSIS_Padawan.Sistema.Boletim.Validations.Rules
{
    public static class NascimentoValidation
    {
        public static bool Validate(DateTime nascimento) => nascimento < new DateTime(2002, 01, 01);
    }
}
