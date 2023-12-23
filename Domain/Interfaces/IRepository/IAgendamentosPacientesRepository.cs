using Domain.Models;

namespace Domain.Interfaces.IRepository
{
    public interface IAgendamentosPacientesRepository : IRepository<AgendamentosPacientes>
    {
        void UpdateDataHoraMarcada(int agendamentoId, DateTime newDataHoraMarcada);
    }
}
