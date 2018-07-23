
using System.ComponentModel.DataAnnotations;

namespace ControleAcesso.ViewModels
{
    public class LoginViewModel
    { 
        [Required(ErrorMessage = "Preencha o usuário")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Preencha a senha")]
        public string Senha { get; set; }
    }
}
