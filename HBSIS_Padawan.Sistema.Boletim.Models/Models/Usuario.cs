using HBSIS_Padawan.Sistema.Boletim.Models.Enums;

namespace HBSIS_Padawan.Sistema.Boletim.Models
{
    public sealed class Usuario
    {
        public int Id { get; set; }            
        public string Login { get; set; }
        public string Senha { get; set; }       
        public TipoUsuario Tipo { get; set; }
    }
}
