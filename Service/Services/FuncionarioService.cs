using Amazon.Lambda.Model;
using AutoMapper;
using Domain.DTOs.FuncionariosDTO;
using Domain.DTOs.PacientesDTO;
using Domain.Interfaces.IRepository;
using Domain.Interfaces.IService;
using Domain.Models;
using Domain.Settings;
using Microsoft.AspNetCore.JsonPatch;
using System.ComponentModel.DataAnnotations;

namespace Service.Services
{
    public class FuncionarioService : IFuncionarioService
    {
        private readonly IFuncionarioRepository _funcionarioRepository;
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IMapper _mapper;
        public FuncionarioService(IFuncionarioRepository funcionarioRepository,
                                  IMapper mapper,
                                  IPacienteRepository pacienteRepository)
        {
            _funcionarioRepository = funcionarioRepository;
            _mapper = mapper;
            _pacienteRepository = pacienteRepository;
        }

        public IList<FuncionarioDTO> ObterTodosFuncionarios()
        {
            IList<Funcionario> funcionarios = _funcionarioRepository.Get();
            IList<FuncionarioDTO> funcionariosDTO = funcionarios.OrderBy(x => x.Nome).Select(p => new FuncionarioDTO
            {
               FuncionarioId = p.FuncionarioId,
               Nome = p.Nome,
               SobreNome = p.SobreNome,
               Idade = p.Idade,
               DataNascimento = p.DataNascimento.ToString("dd/MM/yyyy"),
               CPF = p.CPF,
               Celular = p.Celular,
               Email = p.Email,
               Especialidade = p.Especialidade 
            }).ToList();

            return funcionariosDTO;
        }

        public FuncionarioDTO ObterFuncionarioPorId(int id)
        {
            Funcionario? resultado = _funcionarioRepository.GetById(id);

            if (resultado == null)
                return null;

            FuncionarioDTO funcionarioDTO = new FuncionarioDTO
            {
                FuncionarioId = resultado.FuncionarioId,
                Nome = resultado.Nome,
                SobreNome = resultado.SobreNome,
                Idade = resultado.Idade,
                DataNascimento = resultado.DataNascimento.ToString("dd/MM/yyyy"),
                CPF = resultado.CPF,
                Celular = resultado.Celular,
                Email = resultado.Email,
                Especialidade = resultado.Especialidade
            };

            return funcionarioDTO;
        }

        public ServiceResult CriarFuncionario(Funcionario funcionario)
        {
            var existingFuncionario = _funcionarioRepository.Buscar(c => (c.CPF == funcionario.CPF));

            if (existingFuncionario.Any())
            {
                return new ServiceResult { Success = false, ErrorMessage = "Já existe um funcionário com este CPF." };
            }

            var existingCelular = _funcionarioRepository.Buscar(c => (c.Celular == funcionario.Celular));

            if (existingCelular.Any())
            {
                return new ServiceResult { Success = false, ErrorMessage = "Já existe um funcionário com este número de celular." };
            }

            funcionario.DataNascimento.ToString("dd/MM/yyyy");

            try
            {
                _funcionarioRepository.Add(funcionario);
                return new ServiceResult { Success = true };
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex}");
            }
        }

        public ServiceResult AtualizarFuncionario(FuncionarioDTO funcionarioDTO)
        {
            var existingFuncionario = _funcionarioRepository.Buscar(c => (c.CPF == funcionarioDTO.CPF) && c.FuncionarioId != funcionarioDTO.FuncionarioId);

            if (existingFuncionario.Any())
            {
                return new ServiceResult { Success = false, ErrorMessage = "Já existe um funcionário com este CPF." };
            }

            var existingCelular = _funcionarioRepository.Buscar(c => (c.Celular == funcionarioDTO.Celular) && c.FuncionarioId != funcionarioDTO.FuncionarioId);

            if (existingCelular.Any())
            {
                return new ServiceResult { Success = false, ErrorMessage = "Já existe um funcionário com este número de celular." };
            }

            try
            {
                var funcionarioDb = _funcionarioRepository.GetById(funcionarioDTO.FuncionarioId);

                if (funcionarioDb == null)
                {
                    return new ServiceResult { Success = false, ErrorMessage = "funcionário não encontrado" };
                }

                _mapper.Map(funcionarioDTO, funcionarioDb);

                _funcionarioRepository.Update(funcionarioDb);

                return new ServiceResult { Success = true };
            }
            catch (Exception ex)
            {
                return new ServiceResult { Success = false, ErrorMessage = $"Erro ao atualizar paciente: {ex.Message}" };
            }
        }

        public bool AtualizarFuncionarioParcial(int id, JsonPatchDocument<Funcionario> patchDocument)
        {
            Funcionario? funcionarioId = _funcionarioRepository.GetById(id);

            if (funcionarioId == null)
                return false;

            try
            {
                if (_funcionarioRepository.Buscar(c => (c.CPF == patchDocument.Operations.FirstOrDefault().value) && c.FuncionarioId != id).Any())
                    throw new ValidationException("Documento já cadastrado no sistema.");

                if (_pacienteRepository.Buscar(c => c.CPF == patchDocument.Operations.FirstOrDefault().value).Any())
                    throw new ValidationException("Documento já cadastrado no sistema.");

                // Aplica as modificações contidas no JsonPatchDocument
                patchDocument.ApplyTo(funcionarioId);

                _funcionarioRepository.Update(funcionarioId);

                return true;
            }
            catch (ValidationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new ServiceException("Falha ao atualizar o funcionário.", ex);
            }
        }


        public bool DeletarFuncionario(int funcionarioId)
        {
            Funcionario? funcionario = _funcionarioRepository.GetById(funcionarioId);

            if (funcionario == null)
            {
                return false;
            }

            try
            {
                _funcionarioRepository.Delete(funcionario);
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex}");
            }

            return true;
        }
    }
}
