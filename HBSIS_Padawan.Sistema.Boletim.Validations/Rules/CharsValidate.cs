namespace HBSIS_Padawan.Sistema.Boletim.Validations.Rules
{
    public static class CharsValidate
    {
        public static bool Validate(string entrada)
        {
            bool validar = false;

            foreach (var letra in entrada)
            {
                validar = !char.IsDigit(letra);

                if (!validar)
                    break;
            }
            return validar;
        }
    }
}
