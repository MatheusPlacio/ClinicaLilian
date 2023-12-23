using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Models
{
    public class Procedimento
    {
        public int ProcedimentoId { get; set; }

        //===========================================================================================================================//
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string NomeProcedimento { get; set; }

        //===========================================================================================================================//
        //EF
        [JsonIgnore]
        public List<ProcedimentoAgendamento> ProcedimentoAgendamentos { get; set; }
    }
}
