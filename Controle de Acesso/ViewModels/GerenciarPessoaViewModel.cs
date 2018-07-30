


using ControleAcesso.Models;
using System.ComponentModel.DataAnnotations;

namespace ControleAcesso.ViewModels
{
    public class GerenciarPessoaViewModel
    {
        public Pessoa Pessoa { get; set; }
        public bool ImprimirEtiqueta { get; set; }
    }
}