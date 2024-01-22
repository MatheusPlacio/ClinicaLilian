using Domain.DTOs.AgendamentosDTO;
using Domain.Interfaces.IService;
using Domain.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OpenQA.Selenium;
using Service.Services;
using System.ComponentModel.DataAnnotations;

namespace ClinicaLilian.Controllers
{
    public class AgendamentosController : Controller
    {
        private readonly IAgendamentoService _agendamentoService;
        private readonly IFuncionarioService _funcionarioService;
        private readonly IProcedimentoService _procedimetoService;
        public AgendamentosController(IAgendamentoService agendamentoService,
                                      IFuncionarioService funcionarioService,
                                      IProcedimentoService procedimetoService)
        {
            _agendamentoService = agendamentoService;
            _funcionarioService = funcionarioService;
            _procedimetoService = procedimetoService;
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

        public IActionResult Create()
        {
            ViewBag.Funcionarios = _funcionarioService.BuscarFuncionariosAgendamentos();
            ViewBag.Procedimentos = _procedimetoService.BuscarProcedimentosAgendamentos();
            return View(new AgendamentoFuncionProcedimentosRegisterDTO()); // Aqui, inicialize um novo modelo
        }

        [HttpPost]
        public IActionResult CriarAgendamento(AgendamentoFuncionProcedimentosRegisterDTO agendamentoDTO)
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

        public IActionResult AtualizarAgendamento(int id, DateTime dataHoraMarca)
        {
            try
            {
                var sucesso = _agendamentoService.AtualizarAgendamento(id, dataHoraMarca);

                if (!sucesso.Success)
                {
                    ModelState.AddModelError(string.Empty, sucesso.ErrorMessage);

                    return View("Index", _agendamentoService.ObterTodosAgendamentosFuncionariosProcedimentos());
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        public IActionResult AtualizarConfirmacao(int id)
        {
            try
            {
                var sucesso = _agendamentoService.AtualizarConfirmacao(id);

                if (!sucesso.Success)
                {
                     ModelState.AddModelError(string.Empty, sucesso.ErrorMessage); 

                    return View("Index", _agendamentoService.ObterTodosAgendamentosFuncionariosProcedimentos());
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        public IActionResult AtualizarCancelamento(int id)
        {
            try
            {
                var sucesso = _agendamentoService.AtualizarCancelamento(id);

                if (!sucesso.Success)
                {
                    ModelState.AddModelError(string.Empty, sucesso.ErrorMessage);

                    return View("Index", _agendamentoService.ObterTodosAgendamentosFuncionariosProcedimentos());
                }

                return RedirectToAction("Index");
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
