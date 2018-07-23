using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControleAcesso.Models
{
    public class Usuario
    {
        [Key]
        public int UsuarioID { get; set; }

        public string Username { get; set; }

        public string Senha { get; set; }

        public string Nome { get; set; }

        public DateTime DataCadastro { get; set; }
    }
}
