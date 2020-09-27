using System.Linq;
using System.Text.RegularExpressions;

namespace HBSIS_Padawan.Sistema.Boletim.Validations.Rules
{
    public static class CPFValidate
    {
        public static bool Validate(string cpf)            
        {
            if (cpf is null) return false;

            int[] multiplicador1 = new int[] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf, digito;
            int soma, resto;

            bool match = Regex.IsMatch(cpf, @"^\d{3}\.?\d{3}\.?\d{3}\-?\d{2}$");

            if (match)
            {
                cpf = new string(cpf.Where(char.IsDigit).ToArray());

                tempCpf = cpf.Substring(0, 9);
                soma = 0;

                for (int i = 0; i < multiplicador1.Length; i++)
                    soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

                resto = soma % 11;
                resto = (resto < 2) ? 0 : 11 - resto;

                digito = resto.ToString();
                tempCpf = tempCpf + digito;
                soma = 0;

                for (int i = 0; i < multiplicador2.Length; i++)
                    soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

                resto = soma % 11;
                resto = (resto < 2) ? 0 : 11 - resto;

                digito = digito + resto.ToString();

                return cpf.EndsWith(digito);
            }

            return false;
        }
    }
}
