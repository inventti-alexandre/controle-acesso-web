


using ControleAcesso.Models;
using System.ComponentModel.DataAnnotations;

namespace ControleAcesso.ViewModels
{
    public class NovaPessoaViewModel
    {
        public Pessoa Pessoa { get; set; }
        public bool ImprimirEtiqueta { get; set; } 
    }
}
