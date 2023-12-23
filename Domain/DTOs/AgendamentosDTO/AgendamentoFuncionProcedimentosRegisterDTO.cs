using Domain.DTOs.FuncionariosDTO;
using Domain.DTOs.PacientesDTO;

namespace Domain.DTOs.AgendamentosDTO
{
    public class AgendamentoFuncionProcedimentosRegisterDTO
    {
        public AgendamentoProcediRegisterDTO AgendamentoProcedimentoRegisterDTO { get; set; }

        //===========================================================================================================================//
        public int ProcedimentoId { get; set; }

        //===========================================================================================================================//

        public int PacienteId { get; set; }

        //===========================================================================================================================//
        public int FuncionarioId { get; set; }
    }
}
