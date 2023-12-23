using Domain.Models;

namespace Domain.Interfaces.IRepository
{
    public interface IAgendamentoRepository : IRepository<Agendamento>
    {
        IList<Agendamento> GetTodosAgendamentos();
        Agendamento GetTodosAgendamentosById(int id);
    }
}
