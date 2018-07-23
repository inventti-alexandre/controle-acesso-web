using System;
using System.ComponentModel.DataAnnotations; 

namespace ControleAcesso.Models
{
    public class Evento
    {
        [Key]
        public int EventoID { get; set; }

        public string Nome { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime DataFim { get; set; }

        public string Descricao { get; set; } 

        public string Logo { get; set; }
    }
}
