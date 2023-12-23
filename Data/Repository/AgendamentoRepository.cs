using Data.Context;
using Domain.Interfaces.IRepository;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository
{
    public class AgendamentoRepository : Repository<Agendamento>, IAgendamentoRepository
    { 
        private readonly MyContext _context;
        public AgendamentoRepository(MyContext context) : base(context)
        {
            _context = context;
        }

        public IList<Agendamento> GetTodosAgendamentos()
        {
            var agendamento = _context.Agendamentos.Include(x => x.ProcedimentoAgendamentos)
                                                     .ThenInclude(x => x.Procedimento)
                                                     .Include(x => x.AgendamentosPacientes)
                                                     .ThenInclude(x => x.Paciente)
                                                     .Include(x => x.Funcionario)
                                                     .ToList();

            return agendamento;
        }

        public Agendamento GetTodosAgendamentosById(int id)
        {
           var agendamentoId = _context.Agendamentos.Include(x => x.ProcedimentoAgendamentos)
                                                    .ThenInclude(x => x.Procedimento)
                                                    .Include(x => x.AgendamentosPacientes)
                                                    .ThenInclude(x => x.Paciente)
                                                    .Include(x => x.Funcionario)
                                                    .FirstOrDefault(x => x.AgendamentoId == id);

            return agendamentoId;
        }

    }
}
