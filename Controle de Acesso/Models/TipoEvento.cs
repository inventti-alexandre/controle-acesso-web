using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleAcesso.Models
{
    public class TipoEvento
    {
        [Key]
        public int TipoEventoID { get; set; }
        public string Nome { get; set; } 
    }
}

