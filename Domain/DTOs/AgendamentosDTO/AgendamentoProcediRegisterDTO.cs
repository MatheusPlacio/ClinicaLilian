using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.AgendamentosDTO
{
    public class AgendamentoProcediRegisterDTO
    {
        public DateTime DataHoraMarcada { get; set; }

        //===========================================================================================================================//

        public int Sessoes { get; set; }
        //===========================================================================================================================//


        [StringLength(150, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres")]
        public string Observacao { get; set; }
    }

}
