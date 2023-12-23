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
    [Route("api/pacientes")]
    [ApiController]
    public class PacientesController : ControllerBase
    {
        private readonly IPacienteService _pacienteService;
        private readonly IMapper _mapper;

        public PacientesController(IPacienteService pacienteService, IMapper mapper)
        {
            _pacienteService = pacienteService;
            _mapper = mapper;
        }

        public IActionResult ObterTodosPacientes()
        {
            var pacientes =  _pacienteService.ObterTodosPacientes();

            if (pacientes == null)
                return NotFound("Nenhum paciente encontrado");

            return Ok(pacientes);
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

        public IActionResult ObterTodosPacientesEnderecos()
        {
            var enderecos = _pacienteService.GetTodosPacientesEnderecos();
            return Ok(enderecos);
        }

        public IActionResult CriarPaciente(PacienteRegisterDTO pacienteDTO)
        {
            try
            {
               _pacienteService.CriarPaciente(_mapper.Map<Paciente>(pacienteDTO));
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return CreatedAtAction(nameof(CriarPaciente), new { id = pacienteDTO.PacienteId }, null);
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
