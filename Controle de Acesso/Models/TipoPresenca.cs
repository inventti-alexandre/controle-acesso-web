using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleAcesso.Models
{
    public class TipoPresenca
    {
        [Key]
        public int TipoPresencaID { get; set; }
        public string Nome { get; set; }
        public bool PrivilegiosElevados { get; set; }
    }
}

