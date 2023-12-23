using Amazon.Lambda.Model;
using AutoMapper;
using Domain.DTOs.EnderecoDTO;
using Domain.DTOs.PacientesDTO;
using Domain.Interfaces.IRepository;
using Domain.Interfaces.IService;
using Domain.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.ComponentModel.DataAnnotations;

namespace Service.Services
{
    public class PacienteService : IPacienteService
    {
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IFuncionarioRepository _funcionarioRepository;
        private readonly IMapper _mapper;

        public PacienteService(IPacienteRepository pacienteRepository, IMapper mapper, IFuncionarioRepository funcionarioRepository)
        {
            _pacienteRepository = pacienteRepository;
            _funcionarioRepository = funcionarioRepository;
            _mapper = mapper;
        }

        public IList<PacienteDTO> ObterTodosPacientes()
        {
            var pacientes = _pacienteRepository.Get();
            var pacientesDTO = pacientes.Select(p => new PacienteDTO
            {
                PacienteId = p.PacienteId,
                Nome = p.Nome,
                SobreNome = p.SobreNome,
                DataDeNascimento = p.DataDeNascimento.ToString("dd/MM/yyyy"),
                Genero = p.Genero,
                CPF = p.CPF,
                Email = p.Email,
                Celular = p.Celular,
                Profissao = p.Profissao
            }).ToList();

            return pacientesDTO;
        }

        public PacienteDTO? ObterPacientePorId(int id)
        {
            var resultado = _pacienteRepository.GetById(id);

            if (resultado == null)
                throw new Exception("Paciente não encontrado");

            var pacientesDTO = new PacienteDTO
            {
                PacienteId = resultado.PacienteId,
                Nome = resultado.Nome,
                SobreNome = resultado.SobreNome,
                DataDeNascimento = resultado.DataDeNascimento.ToString("dd/MM/yyyy"),
                Genero = resultado.Genero,
                CPF = resultado.CPF,
                Email = resultado.Email,
                Celular = resultado.Celular,
                Profissao = resultado.Profissao
            };

            return pacientesDTO;
        }

        public void CriarPaciente(Paciente paciente)
        {
            if (_pacienteRepository.Buscar(c => c.CPF == paciente.CPF).Any())
                throw new Exception("Documento já cadastrado no sistema");

            else if (_pacienteRepository.Buscar(c => c.Celular == paciente.Celular).Any())
                throw new Exception("Celular já cadastrado no sistema");

            paciente.DataDeNascimento.ToString("dd/MM/yyyy");

            try
            {
                _pacienteRepository.Add(paciente);
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex}");
            }
        }

        public bool AtualizarPaciente(PacienteUpdateDTO pacienteDTO)
        {
            var existingPaciente = _pacienteRepository.Buscar(c => (c.CPF == pacienteDTO.CPF) && c.PacienteId != pacienteDTO.PacienteId);

            if (existingPaciente.Any())
                return false;

            try
            {
                var pacienteDb = _pacienteRepository.GetById(pacienteDTO.PacienteId);

                if (pacienteDb == null)
                {
                    throw new Exception("Paciente não encontrado");
                }

                _mapper.Map(pacienteDTO, pacienteDb);

                _pacienteRepository.Update(pacienteDb);

                return true;
            }

            catch (Exception ex)
            {
                throw new Exception($"{ex}");
            }
        }

        public bool AtualizarPacienteParcial(int id, JsonPatchDocument<Paciente> patchDocument)
        {
            Paciente? pacienteId = _pacienteRepository.GetById(id);

            if (pacienteId == null)
                return false;

            try
            {
                if (_pacienteRepository.Buscar(c => (c.CPF == patchDocument.Operations.FirstOrDefault().value) && c.PacienteId != id).Any())
                    throw new ValidationException("Documento já cadastrado no sistema.");

                if (_funcionarioRepository.Buscar(c => c.CPF == patchDocument.Operations.FirstOrDefault().value).Any())
                    throw new ValidationException("Documento já cadastrado no sistema.");

                // Aplica as modificações contidas no JsonPatchDocument
                patchDocument.ApplyTo(pacienteId);

                _pacienteRepository.Update(pacienteId);

                return true;
            }
            catch (ValidationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new ServiceException("Falha ao atualizar o paciente.", ex);
            }
        }

        public bool DeletarPaciente(int pacienteId)
        {
            var paciente = _pacienteRepository.GetById(pacienteId);

            if (paciente == null)
                return false;

            try
            {
                _pacienteRepository.Delete(paciente);
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex}");
            }

            return true;
        }

        public IList<PacienteEnderecoDTO> GetTodosPacientesEnderecos()
        {
            IList<Paciente> pacientesEnderecos = _pacienteRepository.GetTodosPacientesEnderecos();

            IList<PacienteEnderecoDTO> pacientesEnderecosDTO = pacientesEnderecos.Select(c => new PacienteEnderecoDTO
            {
                PacienteId = c.PacienteId,
                Nome = c.Nome,
                SobreNome = c.SobreNome,
                DataDeNascimento = c.DataDeNascimento,
                Genero = c.Genero,
                CPF = c.CPF,
                Celular = c.Celular,
                Email = c.Email,
                Profissao = c.Profissao,
                PacienteEndereco = new PacienteEnderecoGetDTO
                {
                    EnderecoId = c.EnderecoId,
                    Logradouro = c.Endereco.Logradouro,
                    Complemento = c.Endereco.Complemento,
                    Numero = c.Endereco.Numero,
                    Cep = c.Endereco.Cep,
                    Bairro = c.Endereco.Bairro,
                    localidade = c.Endereco.localidade,
                    UF = c.Endereco.UF
                }

            }).ToList();

            return pacientesEnderecosDTO;
        }
    }

}

