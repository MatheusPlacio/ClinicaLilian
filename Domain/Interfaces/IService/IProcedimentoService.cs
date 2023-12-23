using Domain.DTOs.ProcedimentosDTO;
using Domain.Models;

namespace Domain.Interfaces.IService
{
    public interface IProcedimentoService
    {
        IList<ProcedimentoDTO> ObterTodosProcedimentos();
        ProcedimentoDTO ObterProcedimentosPorId(int id);
    }
}
