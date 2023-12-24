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

        public IActionResult ObterPacientePorId(int id)
        {
            var paciente =  _pacienteService.ObterPacientePorId(id);
            if (paciente == null)
            {
                return NotFound();
            }
            return Ok(paciente);
        }


        public IActionResult Create()
        {
            // Lógica da Action
            return View();
        }

        [HttpPost]
        public IActionResult CriarPaciente(PacienteRegisterDTO pacienteDTO)
        {
            try
            {
                _pacienteService.CriarPaciente(_mapper.Map<Paciente>(pacienteDTO));

                // Redireciona para a lista de pacientes (por exemplo, a action Index)
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Lida com exceções de maneira apropriada (pode ser interessante logar o erro, exibir uma mensagem ao usuário, etc.)
                ViewBag.ErrorMessage = "Ocorreu um erro ao criar o paciente: " + ex.Message;
                return View("Create", pacienteDTO); // Retorna à página de criação com uma mensagem de erro
            }
        }


        public async Task<IActionResult> AtualizarPaciente(PacienteUpdateDTO pacienteDTO)
        {
            try
            {
                var result = _pacienteService.AtualizarPaciente(pacienteDTO);
                if (!result)
                    return NotFound("Paciente não encontrado");

                return Ok(pacienteDTO);
            }
            catch (Exception ex)
            {

                throw ex;
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

                return Ok("Paciente excluído com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao excluir paciente");
            }
        }
    }


}
