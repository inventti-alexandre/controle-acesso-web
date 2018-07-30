using System.ComponentModel.DataAnnotations;

namespace ControleAcesso.Models
{
    public class TipoUsuario
    {
        [Key]
        public int TipoUsuarioID { get; set; }
        public string Nome { get; set; }
        public bool Administrador { get; set; }
    }
}
