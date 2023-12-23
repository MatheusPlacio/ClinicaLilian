using System.Text.Json.Serialization;

namespace Domain.Models
{
    public class ProcedimentoAgendamento
    {
        public int ProcedimentoAgendamentoId { get; set; }
        //===========================================================================================================================//

        public DateTime DataHoraMarcada { get; set; }
        //===========================================================================================================================//

        public int ProcedimentoId { get; set; }
        public Procedimento Procedimento { get; set; }
        //===========================================================================================================================//

        public int AgendamentoId { get; set; }

        public Agendamento Agendamento { get; set; }
    }
}
