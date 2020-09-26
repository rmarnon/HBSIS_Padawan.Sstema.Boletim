using HBSIS_Padawan.Sistema.Boletim.Models.Enums;

namespace HBSIS_Padawan.Sistema.Boletim.Validations.Rules
{
    public static class StatusMateriaValidation
    {
        public static bool Validate(Status status) => 
            status.ToString() == Status.Ativo.ToString() || status.ToString() == Status.Inativo.ToString();
    }
}
