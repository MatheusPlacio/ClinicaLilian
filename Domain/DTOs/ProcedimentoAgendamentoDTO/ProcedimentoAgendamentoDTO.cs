using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.NewFolder
{
    public class ProcedimentoAgendamentoDTO
    {
        public int ProcedimentoAgendamentoId { get; set; }

        //===========================================================================================================================//
        public int ProcedimentoId { get; set; }

        //===========================================================================================================================//
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string NomeProcedimento { get; set; }

        //===========================================================================================================================//

        public int AgendamentoId { get; set; }
        //===========================================================================================================================//

        public DateTime DataHoraMarcada { get; set; }

        //===========================================================================================================================//

        [StringLength(10, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres")]
        public int Sessoes { get; set; }
        //===========================================================================================================================//


        [StringLength(10, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres")]
        public string Observacao { get; set; }
    }
}
