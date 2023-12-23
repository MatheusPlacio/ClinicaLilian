using Amazon.Lambda.Model;
using AutoMapper;
using Domain.DTOs.FuncionariosDTO;
using Domain.DTOs.PacientesDTO;
using Domain.Interfaces.IRepository;
using Domain.Interfaces.IService;
using Domain.Models;
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
            IList<FuncionarioDTO> funcionariosDTO = funcionarios.Select(p => new FuncionarioDTO
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

        public void CriarFuncionario(Funcionario funcionario)
        {
            if (_funcionarioRepository.Buscar(c => c.CPF == funcionario.CPF).Any())
                throw new Exception("Documento já cadastrado no sistema");

            if (_pacienteRepository.Buscar(c => c.CPF == funcionario.CPF).Any())
                throw new Exception("Documento já cadastrado no sistema");

            else if (_funcionarioRepository.Buscar(c => c.Celular == funcionario.Celular).Any())
                throw new Exception("Celular já cadastrado no sistema");

            funcionario.DataNascimento.ToString("dd/MM/yyyy");

            try
            {
                _funcionarioRepository.Add(funcionario);
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex}");
            }
        }

        public bool AtualizarFuncionario(FuncionarioDTO funcionarioDTO)
        {
            try
            {
                Funcionario? funcionarioDb = _funcionarioRepository.GetById(funcionarioDTO.FuncionarioId);

                if (funcionarioDb == null)
                    return false;

                if (_funcionarioRepository.Buscar(c => (c.CPF == funcionarioDTO.CPF) && c.FuncionarioId != funcionarioDTO.FuncionarioId).Any())
                    throw new ValidationException("Documento já cadastrado no sistema.");

                _mapper.Map(funcionarioDTO, funcionarioDb);

                _funcionarioRepository.Update(funcionarioDb);

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
