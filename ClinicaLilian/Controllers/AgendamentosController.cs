using Domain.DTOs.AgendamentosDTO;
using Domain.Interfaces.IService;
using Domain.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using OpenQA.Selenium;
using System.ComponentModel.DataAnnotations;

namespace ClinicaLilian.Controllers
{
    public class AgendamentosController : Controller
    {
        private readonly IAgendamentoService _agendamentoService;
        public AgendamentosController(IAgendamentoService agendamentoService )
        {
            _agendamentoService = agendamentoService;
        }

        public IActionResult Index()
        {
            var agendamentos = _agendamentoService.ObterTodosAgendamentosFuncionariosProcedimentos();

            if (agendamentos == null)
                return NotFound("Nenhum paciente encontrado");

            return View(agendamentos);
        }

        public IActionResult ObterTodosAgendamentosFuncionariosProcedimentosPorId(int id)
        {
            var agendamentos = _agendamentoService.ObterTodosAgendamentosFuncionariosProcedimentosPorId(id);

            if (agendamentos == null)
            {
                return NotFound("Nenhum agendamento encontrado para o ID especificado.");
            }

            return Ok(agendamentos);
        }

        public IActionResult CriarAgendamento([FromBody] AgendamentoFuncionProcedimentosRegisterDTO agendamentoDTO)
        {
            try
            {
                var novoAgendamento = _agendamentoService.CriarAgendamento(agendamentoDTO);

                if (novoAgendamento == null)
                    return BadRequest("Falha ao criar o agendamento.");

                return CreatedAtAction(nameof(CriarAgendamento), new { id = novoAgendamento.AgendamentoId }, null);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }

        public IActionResult AtualizarAgendamento(int id, [FromBody] JsonPatchDocument<Agendamento> patchDocument)
        {
            try
            {
                bool sucesso = _agendamentoService.AtualizarAgendamento(id, patchDocument);

                if (!sucesso)
                {
                    return NotFound("Agendamento não encontrado.");
                }

                return Ok("Agendamento atualizado com sucesso.");
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        public IActionResult DeletarEndereco(int id)
        {
            var result = _agendamentoService.DeletarAgendamento(id);

            if (!result)
                return BadRequest("Agendamento não encontrado");

            return Ok("Agendamento excluído com sucesso");
        }

        private IActionResult HandleException(Exception ex)
        {
            if (ex is NotFoundException)
            {
                return NotFound(ex.Message);
            }
            else if (ex is ValidationException)
            {
                return BadRequest(ex.Message);
            }
            else
            {
                return StatusCode(500, $"Erro no servidor: {ex.Message}");
            }
        }

    }
}
