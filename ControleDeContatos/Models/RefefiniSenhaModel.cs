using System.ComponentModel.DataAnnotations;

namespace ControleDeContatos.Models
{
    public class RefefiniSenhaModel
    {
        [Required(ErrorMessage = "Digite o login")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Digite o email")]
        public string Email { get; set; }
    }
}
