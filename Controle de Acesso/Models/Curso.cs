using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleAcesso.Models
{
    public class Curso
    {
        [Key]
        public int CursoID { get; set; }
        public string Nome { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public string Descricao { get; set; }
        public string Logo { get; set; }
        public int EventoID { get; set; } 
        [ForeignKey("EventoID")]
        public virtual Evento Evento { get; set; }
    }
}
