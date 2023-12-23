using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.FuncionariosDTO
{
    public class FuncionarioDTO
    {
        public int FuncionarioId { get; set; }

        //===========================================================================================================================//

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Nome { get; set; }

        //===========================================================================================================================//

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string SobreNome { get; set; }

        //===========================================================================================================================//

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int Idade { get; set; }

        //===========================================================================================================================//

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DataType(DataType.Date)]
        public string DataNascimento { get; set; }

        //===========================================================================================================================//

        [StringLength(11, ErrorMessage = "O campo {0} precisa ter 11 dígitos", MinimumLength = 11)]
        public string CPF { get; set; }

        //===========================================================================================================================//

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(14, ErrorMessage = "O campo {0} precisa ter no mínimo 11 dígitos", MinimumLength = 11)]
        public string Celular { get; set; }

        //===========================================================================================================================//

        [StringLength(70, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres")]
        public string Email { get; set; }

        //===========================================================================================================================//

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Especialidade { get; set; }
    }
}
