using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ControleAcesso.Models
{
    public class Evento
    {
        [Key]
        public int EventoID { get; set; }

        [DisplayName("Nome")]
        public string Nome { get; set; }

        [DisplayName("Data início")]
        public DateTime DataInicio { get; set; }

        [DisplayName("Data fim")]
        public DateTime DataFim { get; set; }

        [DisplayName("Descrição")]
        public string Descricao { get; set; }

        [DisplayName("Logo")]
        public string Logo { get; set; }

        [DisplayName("Banner")]
        public string Banner { get; set; }
    }
}
