using Amazon.Lambda.Model;
using AutoMapper;
using Domain.DTOs.FuncionariosDTO;
using Domain.DTOs.PacientesDTO;
using Domain.Interfaces.IService;
using Domain.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using OpenQA.Selenium;
using Service.Services;
using System.ComponentModel.DataAnnotations;

namespace ClinicaLilian.Controllers
{
    [Route("api/funcionarios")]
    [ApiController]
    public class FuncionariosController : ControllerBase
    {
        private readonly IFuncionarioService _funcionarioService;
        private readonly IMapper _mapper;

        public FuncionariosController(IFuncionarioService funcionarioService, IMapper mapper)
        {
            _funcionarioService = funcionarioService;
            _mapper = mapper;
        }

        public IActionResult ObterTodosFuncionarios()
        {
            var funcionarios = _funcionarioService.ObterTodosFuncionarios();

            if (funcionarios == null)
                return NotFound("Nenhum funcionário encontrado");

            return Ok(funcionarios);
        }

        public IActionResult ObterFuncionarioPorId(int id)
        {
            var funcionario = _funcionarioService.ObterFuncionarioPorId(id);
            if (funcionario == null)
            {
                return NotFound("Funcionário não encontrado");
            }
            return Ok(funcionario);
        }

        public IActionResult CriarFuncionario(FuncionarioDTO funcionarioDTO)
        {
            try
            {
                _funcionarioService.CriarFuncionario(_mapper.Map<Funcionario>(funcionarioDTO));
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return CreatedAtAction(nameof(CriarFuncionario), new { id = funcionarioDTO.FuncionarioId }, null);
        }

        public async Task<IActionResult> AtualizarFuncionario(FuncionarioDTO funcionarioDTO)
        {
            try
            {
                var result = _funcionarioService.AtualizarFuncionario(funcionarioDTO);
                if (!result)
                    return BadRequest("Funcionário não encontrado");

                return Ok(funcionarioDTO);
            }

            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }

            catch (Exception ex)
            {
                return StatusCode(500, $"Erro no servidor: {ex.Message}");
            }
        }

        public IActionResult AtualizarFuncionarioParcial(int id, [FromBody] JsonPatchDocument<Funcionario> patchDocument)
        {
            try
            {
                bool sucesso = _funcionarioService.AtualizarFuncionarioParcial(id, patchDocument);

                if (!sucesso)
                {
                    return NotFound("Funcionário não encontrado.");
                }

                return Ok("Funcionário atualizado com sucesso.");
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
                var result = _funcionarioService.DeletarFuncionario(id);
                if (!result)
                    return NotFound("Funcionário não encontrado");

                return Ok("Funcionário excluído com sucesso");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}

