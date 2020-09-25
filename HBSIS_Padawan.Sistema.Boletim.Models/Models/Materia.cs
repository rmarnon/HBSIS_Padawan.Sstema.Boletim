using HBSIS_Padawan.Sistema.Boletim.Models.Enums;
using System;

namespace HBSIS_Padawan.Sistema.Boletim.Models
{
    public class Materia
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime Cadastro { get; set; }
        public Status Status { get; set; }
    }
}
