using Amazon.Lambda.Model;
using Data.Context;
using Domain.Interfaces.IRepository;
using Domain.Models;

namespace Data.Repository
{
    public class ProcedimentoAgendamentosRepository : Repository<ProcedimentoAgendamento>, IProcedimentoAgendamentosRepository
    {
        private readonly MyContext _context;
        public ProcedimentoAgendamentosRepository(MyContext context) : base(context)
        {
            _context = context;
        }

        public void UpdateDataHoraMarcada(int agendamentoId, DateTime newDataHoraMarcada)
        {
            ProcedimentoAgendamento? procedimentoAgendamento = _context.ProcedimentosAgendamentos.FirstOrDefault(x => x.AgendamentoId == agendamentoId);

            try
            {
                if (procedimentoAgendamento != null)
                {
                    procedimentoAgendamento.DataHoraMarcada = newDataHoraMarcada;
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new ServiceException("Falha ao atualizar DataHoraMarcada em ProcedimentoAgendamento.", ex);
            }
        }

    }
}
