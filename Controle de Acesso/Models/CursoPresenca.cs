using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleAcesso.Models
{
    public class CursoPresenca
    {
        [Key]
        public int CursoPresencaID { get; set; }
        public int PessoaID { get; set; }
        [ForeignKey("PessoaID")]
        public virtual Pessoa Pessoa { get; set; } 
        public int CursoID { get; set; }
        [ForeignKey("CursoID")]
        public virtual Curso Curso { get; set; } 
        public int StatusID { get; set; }
        [ForeignKey("StatusID")]
        public virtual Status Status { get; set; }  
        public int TipoPresencaID { get; set; }
        [ForeignKey("TipoPresencaID")]
        public virtual TipoPresenca TipoPresenca { get; set; }
    }
}
