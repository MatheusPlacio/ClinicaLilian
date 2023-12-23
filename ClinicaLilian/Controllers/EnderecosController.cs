using AutoMapper;
using Domain.DTOs.EnderecoDTO;
using Domain.Interfaces.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaLilian.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnderecosController : ControllerBase
    {
        private readonly IEnderecoService _enderecoService;
        private readonly IMapper _mapper;

        public EnderecosController(IEnderecoService enderecoService, IMapper mapper)
        {
            _enderecoService = enderecoService;
            _mapper = mapper;
        }

        public IActionResult ObterTodosEnderecos()
        {
            var enderecos = _enderecoService.GetTodosEnderecos();
            return Ok(enderecos);
        }

        public IActionResult ObterEnderecoPorId(int id)
        {
            var enderecos = _enderecoService.ObterEnderecoPorId(id);
            if (enderecos == null)
            {
                return NotFound("Endereço não encontrado");
            }
            return Ok(enderecos);
        }

        public IActionResult AdicionarEndereco(EnderecoCepDTO cep)
        {
            try
            {
               _enderecoService.AdicionarEndereco(cep);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Ok();
        }

        public IActionResult AtualizarEndereco(EnderecoDTO endereco)
        {
            var result = _enderecoService.AtualizarEndereco(endereco);
            if (!result)
            {
                return BadRequest("Endereço não encontrado");
            }
            return Ok(endereco);
        }

        public IActionResult DeletarEndereco(int id)
        {
            var result = _enderecoService.DeletarEndereco(id);
            if (!result)
            {
                return BadRequest("Endereço não encontrado");
            }
            return Ok("Endereço excluído com sucesso");
        }

    }
}
