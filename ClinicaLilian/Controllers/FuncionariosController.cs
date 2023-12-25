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
    public class FuncionariosController : Controller
    {
        private readonly IFuncionarioService _funcionarioService;
        private readonly IMapper _mapper;

        public FuncionariosController(IFuncionarioService funcionarioService, IMapper mapper)
        {
            _funcionarioService = funcionarioService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var funcionarios = _funcionarioService.ObterTodosFuncionarios();

            if (funcionarios == null)
                return NotFound("Nenhum funcionário encontrado");

            return View(funcionarios);
        }

        public IActionResult ObterFuncionarioPorId(string id)
        {
            if (int.TryParse(id, out int funcionarioId))
            {
                var funcionario = _funcionarioService.ObterFuncionarioPorId(funcionarioId);
                if (funcionario == null)
                {
                    return NotFound();
                }
                return View("Details", funcionario);
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
        public IActionResult CriarFuncionario(FuncionarioDTO funcionarioDTO)
        {
            try
            {
                var result = _funcionarioService.CriarFuncionario(_mapper.Map<Funcionario>(funcionarioDTO));
                if (!result.Success)
                {
                    // Se houver erros de validação, adicione mensagens ao ModelState
                    if (!string.IsNullOrEmpty(result.ErrorMessage))
                    {
                        ModelState.AddModelError(string.Empty, result.ErrorMessage);
                    }

                    return View("Create", funcionarioDTO);
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Ocorreu um erro ao criar o paciente: " + ex.Message;
                return View("Create", funcionarioDTO); // Retorna à página de criação com uma mensagem de erro
            }
        }

        public IActionResult Edit(int id)
        {
            var funcionario = _funcionarioService.ObterFuncionarioPorId(id);
            if (funcionario == null)
            {
                return NotFound();
            }

            // Aqui você pode retornar a view com o formulário de edição
            return View("Edit", funcionario);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(FuncionarioDTO funcionarioDTO)
        {
            try
            {
                var result = _funcionarioService.AtualizarFuncionario(funcionarioDTO);
                if (!result.Success)
                {
                    // Se houver erros de validação, adicione mensagens ao ModelState
                    if (!string.IsNullOrEmpty(result.ErrorMessage))
                    {
                        ModelState.AddModelError(string.Empty, result.ErrorMessage);
                    }

                    // Retorne a view com os erros
                    return View(funcionarioDTO);
                }

                // Aqui você redireciona para a view de detalhes (ou outra view desejada)
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Trate exceções adequadamente, por exemplo, log ou retornar BadRequest
                return BadRequest($"Erro ao atualizar o funcionário: {ex.Message}");
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

        public IActionResult DeletarFuncionario(int id)
        {
            try
            {
                var result = _funcionarioService.DeletarFuncionario(id);
                if (!result)
                    return NotFound("Funcionário não encontrado");

                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                return BadRequest("Erro ao excluir paciente");
            }
        }

    }
}

