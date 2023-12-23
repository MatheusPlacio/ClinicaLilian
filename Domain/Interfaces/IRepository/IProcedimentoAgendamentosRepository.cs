using Domain.Models;

namespace Domain.Interfaces.IRepository
{
    public interface IProcedimentoAgendamentosRepository : IRepository<ProcedimentoAgendamento>
    {
        void UpdateDataHoraMarcada(int agendamentoId, DateTime newDataHoraMarcada);
    }
}
