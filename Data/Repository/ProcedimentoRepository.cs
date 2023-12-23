using Data.Context;
using Domain.Interfaces.IRepository;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository
{
    public class ProcedimentoRepository : Repository<Procedimento>, IProcedimentoRepository
    {
        private readonly MyContext _context;
        public ProcedimentoRepository(MyContext context) : base(context)
        {
            _context = context;
        }

        public IList<Procedimento> GetProcedimentosAgendamentos()
        {
            var procedimentos = _context.Procedimentos.Include(x => x.ProcedimentoAgendamentos).ToList();

            return procedimentos;
        }
    }
}
