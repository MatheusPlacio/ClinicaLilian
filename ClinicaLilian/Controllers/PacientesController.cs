using Amazon.Lambda.Model;
using AutoMapper;
using Domain.DTOs.PacientesDTO;
using Domain.Interfaces.IService;
using Domain.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Service.Services;
using System.ComponentModel.DataAnnotations;

namespace ClinicaLilian.Controllers
{
    public class PacientesController : Controller
    {
        private readonly IPacienteService _pacienteService;
        private readonly IMapper _mapper;

        public PacientesController(IPacienteService pacienteService, IMapper mapper)
        {
            _pacienteService = pacienteService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var pacientes =  _pacienteService.ObterTodosPacientes();
            if (pacientes == null)
                return NotFound("Nenhum paciente encontrado");

            return View(pacientes);
        }

        public IActionResult PesquisarPorNome(string searchNome)
        {
            var pacientes = _pacienteService.ObterTodosPacientes();
            if (pacientes == null)
                return NotFound("Nenhum paciente encontrado");

            return View(pacientes); 
        }

        [HttpGet("PesquisarPorCpf")]
        public IActionResult PesquisarPorCpf(string searchCpf)
        {
            if (string.IsNullOrEmpty(searchCpf))
            {
                return BadRequest("CPF de pesquisa inválido");
            }

            var pacientes = _pacienteService.ObterPacientePeloCPF(searchCpf);

            return View("Index", pacientes); // Reutiliza a view Index, pois a lógica é semelhante
        }

        public IActionResult ObterPacientePorId(string id)
        {
            if (int.TryParse(id, out int pacienteId))
            {
                var paciente = _pacienteService.ObterPacientePorId(pacienteId);
                if (paciente == null)
                {
                    return NotFound();
                }
                return View("Details", paciente);
            }
            else
            {
                return BadRequest("ID inválido");
            }
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CriarPaciente(PacienteRegisterDTO pacienteDTO)
        {
            try
            {               
                var result = _pacienteService.CriarPaciente(_mapper.Map<Paciente>(pacienteDTO));
                if (!result.Success)
                {
                    // Se houver erros de validação, adicione mensagens ao ModelState
                    if (!string.IsNullOrEmpty(result.ErrorMessage))
                    {
                        ModelState.AddModelError(string.Empty, result.ErrorMessage);
                    }

                    return View("Create", pacienteDTO);
                }

                // Aqui você redireciona para a view de detalhes (ou outra view desejada)
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Ocorreu um erro ao criar o paciente: " + ex.Message;
                return View("Create", pacienteDTO); // Retorna à página de criação com uma mensagem de erro
            }
        }

        public IActionResult Edit(int id)
        {
            var paciente = _pacienteService.ObterPacientePorId(id);
            if (paciente == null)
            {
                return NotFound();
            }

            var pacienteUpdateDTO = _mapper.Map<PacienteUpdateDTO>(paciente);

            // Aqui você pode retornar a view com o formulário de edição
            return View("Edit", pacienteUpdateDTO);
        }

        [HttpPost]
        public IActionResult Edit(PacienteUpdateDTO pacienteDTO)
        {
            try
            {
                var result = _pacienteService.AtualizarPaciente(pacienteDTO);
                if (!result.Success)
                {
                    // Se houver erros de validação, adicione mensagens ao ModelState
                    if (!string.IsNullOrEmpty(result.ErrorMessage))
                    {
                        ModelState.AddModelError(string.Empty, result.ErrorMessage);
                    }

                    // Retorne a view com os erros
                    return View(pacienteDTO);
                }

                // Aqui você redireciona para a view de detalhes (ou outra view desejada)
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Trate exceções adequadamente, por exemplo, log ou retornar BadRequest
                return BadRequest($"Erro ao atualizar paciente: {ex.Message}");
            }
        }

        public IActionResult AtualizarFuncionarioParcial(int id, [FromBody] JsonPatchDocument<Paciente> patchDocument)
        {
            try
            {
                bool sucesso = _pacienteService.AtualizarPacienteParcial(id, patchDocument);

                if (!sucesso)
                {
                    return NotFound("Paciente não encontrado.");
                }

                return Ok("Paciente atualizado com sucesso.");
            }

            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }

            catch (ServiceException ex)
            {
                return StatusCode(500, $"Erro no servidor: {ex.Message}");
            }
        }

        public IActionResult DeletarPaciente(int id)
        {
            try
            {
                var result = _pacienteService.DeletarPaciente(id);
                if (!result)
                    return NotFound("Paciente não encontrado");

                // Redirecionar para a ação Index do controlador Pacientes
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return BadRequest("Erro ao excluir paciente");
            }
        }

    }


}
