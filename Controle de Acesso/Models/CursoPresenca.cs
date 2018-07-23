using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleAcesso.Models
{
    public class CursoPresenca
    {
        [Key]
        public int CursoPresencaID { get; set; } 

        public string CPF { get; set; }

        public string Nome { get; set; }

        public string NomeCracha { get; set; }

        public string Empresa { get; set; }

        public int CursoID { get; set; }

        [ForeignKey("CursoID")]
        public virtual Curso Curso { get; set; }
    }
}
