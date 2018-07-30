using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleAcesso.Models
{
    public class Pessoa
    {
        [Key]
        public int PessoaID { get; set; }  

        [Required(ErrorMessage = "CPF deve ser preenchido")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "Nome deve ser preenchido")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Nome no crachá deve ser preenchido")]
        public string NomeCracha { get; set; } 

        public string CNPJ { get; set; }

        [Required(ErrorMessage = "Farmácia deve ser preenchida")]
        public string Empresa { get; set; }

        [Required(ErrorMessage = "Cargo deve ser preenchido")]
        public string Cargo { get; set; }

        public string CEP { get; set; }
        public string Endereco { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }

        [Required(ErrorMessage = "Telefone deve ser preenchido")]
        public string Telefone { get; set; } 
        public string Celular { get; set; }

        [Required(ErrorMessage = "E-mail deve ser preenchido")]
        public string Email { get; set; } 
        
        public int StatusID { get; set; }
        public virtual Status Status { get; set; }
    }
}
