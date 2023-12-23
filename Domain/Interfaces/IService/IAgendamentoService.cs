using Domain.DTOs.AgendamentosDTO;
using Domain.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace Domain.Interfaces.IService
{
    public interface IAgendamentoService
    {
        IList<AgendamentoFuncionProcedimentosDTO> ObterTodosAgendamentosFuncionariosProcedimentos();
        AgendamentoFuncionProcedimentosDTO? ObterTodosAgendamentosFuncionariosProcedimentosPorId(int id);
        Agendamento CriarAgendamento(AgendamentoFuncionProcedimentosRegisterDTO agendamentoDTO);
        bool AtualizarAgendamento(int id, JsonPatchDocument<Agendamento> patchDocument);
        bool DeletarAgendamento(int id);
    }
}
