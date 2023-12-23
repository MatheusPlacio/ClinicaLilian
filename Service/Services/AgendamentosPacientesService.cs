using Domain.Interfaces.IRepository;
using Domain.Interfaces.IService;

namespace Service.Services
{
    public class AgendamentosPacientesService : IAgendamentosPacientesService
    {
        private readonly IAgendamentosPacientesRepository _agendamentosPacientesRepository;
        public AgendamentosPacientesService(IAgendamentosPacientesRepository agendamentosPacientesRepository)
        {
            _agendamentosPacientesRepository = agendamentosPacientesRepository;
        }

    }
}
