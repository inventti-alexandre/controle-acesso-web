using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleAcesso.Models
{
    public class Status
    {
        [Key]
        public int StatusID { get; set; }
        public string Nome { get; set; }         
        public bool Bloquear { get; set; } 
    }
}
