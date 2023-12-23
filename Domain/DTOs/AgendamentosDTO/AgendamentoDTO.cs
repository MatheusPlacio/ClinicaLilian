using Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.AgendamentosDTO
{
    public class AgendamentoDTO
    {
        public int AgendamentoId { get; set; }
        //===========================================================================================================================//

        public DateTime DataHoraMarcada { get; set; }

        //===========================================================================================================================//

        [StringLength(10, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres")]
        public int Sessoes { get; set; }
        //===========================================================================================================================//


        [StringLength(150, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres")]
        public string Observacao { get; set; }
        //===========================================================================================================================//
    }
}

