using Domain.DTOs.FuncionariosDTO;
using Domain.DTOs.NewFolder;
using Domain.DTOs.PacientesDTO;

namespace Domain.DTOs.AgendamentosDTO
{
    public class AgendamentoFuncionProcedimentosDTO
    {
        public ProcedimentoAgendamentoDTO ProcedimentoAgendamento { get; set; }

        //===========================================================================================================================//

        public PacienteDTO Paciente { get; set; }

        //===========================================================================================================================//
        public FuncionarioDTO Funcionario { get; set; }
     
    }
}
