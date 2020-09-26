namespace HBSIS_Padawan.Sistema.Boletim.Validations.Rules
{
    public static class CharsValidate
    {
        public static bool Validate(string letras)
        {
            bool validar = false;

            if (letras is null)
                return validar;

            foreach (var letra in letras)
            {
                validar = !char.IsDigit(letra);

                if (!validar)
                    break;
            }
            return validar;
        }
    }
}
