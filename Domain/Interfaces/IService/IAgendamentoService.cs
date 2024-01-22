using Domain.DTOs.AgendamentosDTO;
using Domain.Models;
using Domain.Settings;
using Microsoft.AspNetCore.JsonPatch;

namespace Domain.Interfaces.IService
{
    public interface IAgendamentoService
    {
        IList<AgendamentoFuncionProcedimentosDTO> ObterTodosAgendamentosFuncionariosProcedimentos();
        AgendamentoFuncionProcedimentosDTO? ObterTodosAgendamentosFuncionariosProcedimentosPorId(int id);
        Agendamento CriarAgendamento(AgendamentoFuncionProcedimentosRegisterDTO agendamentoDTO);
        ServiceResult AtualizarConfirmacao(int id);
        ServiceResult AtualizarAgendamento(int id, DateTime dataHoraMarca);
        ServiceResult AtualizarCancelamento(int agendamentoId);
        bool DeletarAgendamento(int id);
    }
}
