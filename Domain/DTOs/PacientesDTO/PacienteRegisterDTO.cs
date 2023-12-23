using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.PacientesDTO
{
    public class PacienteRegisterDTO
    {
        public int PacienteId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(30, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres")]
        public string Nome { get; set; }

        //============================================================================================================================//

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres")]
        public string SobreNome { get; set; }

        //============================================================================================================================//

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DataType(DataType.Date)]
        public DateTime DataDeNascimento { get; set; }

        //============================================================================================================================//

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(30, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres")]
        public string Genero { get; set; }

        //============================================================================================================================//

        [StringLength(11, ErrorMessage = "O campo {0} precisa ter 11 dígitos", MinimumLength = 11)]
        public string CPF { get; set; }

        //============================================================================================================================//

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(14, ErrorMessage = "O campo {0} precisa ter no mínimo 11 dígitos", MinimumLength = 11)]
        public string Celular { get; set; }

        //============================================================================================================================//

        [StringLength(70, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres")]
        public string Email { get; set; }

        //============================================================================================================================//
        public string? Profissao { get; set; }

        //============================================================================================================================//

        public int? Idade { get; set; }

        //============================================================================================================================//
        public int EnderecoId { get; set; }

    }
}
