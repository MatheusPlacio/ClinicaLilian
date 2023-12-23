using Amazon.Lambda.Model;
using Data.Context;
using Domain.Interfaces.IRepository;
using Domain.Models;

namespace Data.Repository
{
    public class AgendamentosPacientesRepository : Repository<AgendamentosPacientes>, IAgendamentosPacientesRepository
    {
        private readonly MyContext _context;
        public AgendamentosPacientesRepository(MyContext context) : base(context)
        {
            _context = context;
        }

        public void UpdateDataHoraMarcada(int agendamentoId, DateTime newDataHoraMarcada)
        {
            AgendamentosPacientes? agendamentoPaciente = _context.AgendamentosPacientes.FirstOrDefault(x => x.AgendamentoId == agendamentoId);

            try
            {
                if (agendamentoPaciente != null)
                {
                    agendamentoPaciente.DataHoraMarcada = newDataHoraMarcada;
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new ServiceException("Falha ao atualizar DataHoraMarcada em AgendamentosPacientes.", ex);
            }
        }

    }
}
