using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.LoginDTO
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [EmailAddress(ErrorMessage = "O campo {0} não é um endereço de e-mail válido.")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }



        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }



        [Display(Name = "Lembrar-me")]
        public bool RememberMe { get; set; }
    }
}
