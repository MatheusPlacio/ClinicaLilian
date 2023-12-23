using System.Text.Json.Serialization;

namespace Domain.Models
{
    public class AgendamentosPacientes
    {
        public int AgendamentosPacientesId { get; set; }

        public int AgendamentoId { get; set; }

        public Agendamento Agendamento { get; set; }

        public int PacienteId { get; set; }
        public Paciente Paciente { get; set; }

        //===========================================================================================================================//
        public DateTime DataHoraMarcada { get; set; }
       
    }

}
