using Domain.Models;

namespace Domain.Interfaces.IRepository
{
    public interface IProcedimentoRepository : IRepository<Procedimento>
    {
        IList<Procedimento> GetProcedimentosAgendamentos();
    }
}
