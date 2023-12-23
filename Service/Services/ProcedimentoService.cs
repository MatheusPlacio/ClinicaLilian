using Domain.DTOs.AgendamentosDTO;
using Domain.DTOs.ProcedimentosDTO;
using Domain.Interfaces.IRepository;
using Domain.Interfaces.IService;
using Domain.Models;

namespace Service.Services
{
    public class ProcedimentoService : IProcedimentoService
    {
        private readonly IProcedimentoRepository _procedimentoRepository;

        public ProcedimentoService(IProcedimentoRepository procedimentoRepository)
        {
            _procedimentoRepository = procedimentoRepository;
        }

        public IList<ProcedimentoDTO> ObterTodosProcedimentos()
        {
            IList<Procedimento> procedimentos = _procedimentoRepository.Get();

            IList<ProcedimentoDTO> procedimentoDTO = procedimentos.Select(p => new ProcedimentoDTO
            {
                ProcedimentoId = p.ProcedimentoId,
                NomeProcedimento = p.NomeProcedimento
            }).ToList();

            return procedimentoDTO;
        }

        public ProcedimentoDTO ObterProcedimentosPorId(int id)
        {
            var procedimentos = _procedimentoRepository.GetById(id);

            ProcedimentoDTO procedimentoDTO = new ProcedimentoDTO
            {
                ProcedimentoId = procedimentos.ProcedimentoId,
                NomeProcedimento = procedimentos.NomeProcedimento
            };

            return procedimentoDTO;
        }

    }
}
