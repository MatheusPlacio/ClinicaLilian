using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.ProcedimentosDTO
{
    public class ProcedimentoDTO
    {
        public int ProcedimentoId { get; set; }

        //===========================================================================================================================//
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string NomeProcedimento { get; set; }
    }
}
