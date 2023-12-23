using Domain.Interfaces.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Services;

namespace ClinicaLilian.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcedimentosController : ControllerBase
    {
        private readonly IProcedimentoService _procedimentoService;

        public ProcedimentosController(IProcedimentoService procedimentoService)
        {
            _procedimentoService = procedimentoService;
        }

        public IActionResult ObterTodosProcedimentos()
        {
            var procedimentos = _procedimentoService.ObterTodosProcedimentos();

            if (procedimentos == null)
                return NotFound("Nenhum procedimento encontrado");

            return Ok(procedimentos);
        }

        public IActionResult ObterTodosProcedimentosAgendamentos(int id)
        {
            var procedimentos = _procedimentoService.ObterProcedimentosPorId(id);

            if (procedimentos == null)
                return NotFound("Nenhum procedimento encontrado");

            return Ok(procedimentos);
        }
    }
}
