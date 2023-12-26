using Amazon.Lambda.Model;
using Domain.DTOs.AgendamentosDTO;
using Domain.DTOs.FuncionariosDTO;
using Domain.DTOs.NewFolder;
using Domain.DTOs.PacientesDTO;
using Domain.Interfaces.IRepository;
using Domain.Interfaces.IService;
using Domain.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Service.Services
{
    public class AgendamentoService : IAgendamentoService
    {
        private readonly IAgendamentoRepository _agendamentoRepository;
        private readonly IProcedimentoRepository _procedimentoRepository;
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IFuncionarioRepository _funcionarioRepository;
        private readonly IProcedimentoAgendamentosRepository _procedimentoAgendamentosRepository;
        private readonly IAgendamentosPacientesRepository _agendamentosPacientesRepository;
        private readonly IAgendamentosPacientesService _agendamentosPacientesService;
        private readonly ILogger<AgendamentoService> _logger;

        public AgendamentoService(IAgendamentoRepository agendamentoRepository,
                                 IProcedimentoRepository procedimentoRepository,
                                 IPacienteRepository pacienteRepository,
                                 IFuncionarioRepository funcionarioRepository,
                                 IProcedimentoAgendamentosRepository procedimentoAgendamentosRepository,
                                 IAgendamentosPacientesRepository agendamentosPacientesRepository,
                                 IAgendamentosPacientesService agendamentosPacientesService,
                                 ILogger<AgendamentoService> logger)
        {
            _agendamentoRepository = agendamentoRepository;
            _procedimentoRepository = procedimentoRepository;
            _pacienteRepository = pacienteRepository;
            _funcionarioRepository = funcionarioRepository;
            _procedimentoAgendamentosRepository = procedimentoAgendamentosRepository;
            _agendamentosPacientesRepository = agendamentosPacientesRepository;
            _agendamentosPacientesService = agendamentosPacientesService;
            _logger = logger;
        }

        public IList<AgendamentoFuncionProcedimentosDTO> ObterTodosAgendamentosFuncionariosProcedimentos()
        {
            IList<Agendamento> procedimentoAgendamentos = _agendamentoRepository.GetTodosAgendamentos();

            var agendamentoFuncionProcedimentosDTO = procedimentoAgendamentos.Select(p => new AgendamentoFuncionProcedimentosDTO
            {
                ProcedimentoAgendamento = new ProcedimentoAgendamentoDTO
                {
                    ProcedimentoAgendamentoId = p.ProcedimentoAgendamentos.FirstOrDefault().ProcedimentoAgendamentoId,
                    ProcedimentoId = p.ProcedimentoAgendamentos.FirstOrDefault().ProcedimentoId,
                    NomeProcedimento = p.ProcedimentoAgendamentos.FirstOrDefault().Procedimento.NomeProcedimento,
                    AgendamentoId = p.AgendamentoId,
                    DataHoraMarcada = p.DataHoraMarcada,
                    Sessoes = p.Sessoes,
                    Observacao = p.Observacao,
                    Realizado = p.Realizado,
                    Cancelado = p.Cancelado
                },
                Paciente = new PacienteDTO
                {
                    PacienteId = p.AgendamentosPacientes.FirstOrDefault()?.Paciente.PacienteId ?? 0,
                    Idade = p.AgendamentosPacientes.FirstOrDefault()?.Paciente.Idade ?? 0,
                    Nome = p.AgendamentosPacientes.FirstOrDefault()?.Paciente.Nome ?? "Nome não informado",
                    SobreNome = p.AgendamentosPacientes.FirstOrDefault()?.Paciente.SobreNome ?? "SobreNome não informado",
                    DataDeNascimento = p.AgendamentosPacientes.FirstOrDefault()?.Paciente.DataDeNascimento.ToString("dd/MM/yyyy") ?? "Data de Nascimento não informada",
                    Genero = p.AgendamentosPacientes.FirstOrDefault()?.Paciente.Genero ?? "Genero não informado",
                    CPF = p.AgendamentosPacientes.FirstOrDefault()?.Paciente.CPF ?? "CPF não informado",
                    Celular = p.AgendamentosPacientes.FirstOrDefault()?.Paciente.Celular ?? "Celular não informado",
                    Email = p.AgendamentosPacientes.FirstOrDefault()?.Paciente.Email ?? "Email não informado",
                    Profissao = p.AgendamentosPacientes.FirstOrDefault()?.Paciente.Profissao ?? "Profissão não informado",
                },
                Funcionario = new FuncionarioDTO
                {
                    FuncionarioId = p.Funcionario.FuncionarioId,
                    Nome = p.Funcionario.Nome,
                    SobreNome = p.Funcionario.SobreNome,
                    Idade = p.Funcionario.Idade,
                    CPF = p.Funcionario.CPF,
                    Celular = p.Funcionario.Celular,
                    Email = p.Funcionario.Email,
                    DataNascimento = p.Funcionario.DataNascimento.ToString("dd/MM/yyyy"),
                    Especialidade = p.Funcionario.Especialidade
                },
            }).ToList();

            _logger.LogInformation($"Informações do agendamentoFuncionProcedimentosDTO: {JsonConvert.SerializeObject(agendamentoFuncionProcedimentosDTO)}");

            return agendamentoFuncionProcedimentosDTO;
        }

        public AgendamentoFuncionProcedimentosDTO? ObterTodosAgendamentosFuncionariosProcedimentosPorId(int id)
        {
            Agendamento? agendamento = _agendamentoRepository.GetTodosAgendamentosById(id);

            if (agendamento == null)
                return null;

            var agendamentoFuncionProcedimentosDTO = new AgendamentoFuncionProcedimentosDTO
            {
                ProcedimentoAgendamento = new ProcedimentoAgendamentoDTO
                {
                    ProcedimentoAgendamentoId = agendamento.ProcedimentoAgendamentos.FirstOrDefault().ProcedimentoAgendamentoId,
                    ProcedimentoId = agendamento.ProcedimentoAgendamentos.FirstOrDefault().ProcedimentoId,
                    NomeProcedimento = agendamento.ProcedimentoAgendamentos.FirstOrDefault().Procedimento.NomeProcedimento,
                    AgendamentoId = agendamento.AgendamentoId,
                    DataHoraMarcada = agendamento.DataHoraMarcada,
                    Sessoes = agendamento.Sessoes,
                    Observacao = agendamento.Observacao,
                },
                Paciente = new PacienteDTO
                {
                    PacienteId = agendamento.AgendamentosPacientes.FirstOrDefault()?.Paciente.PacienteId ?? 0,
                    Nome = agendamento.AgendamentosPacientes.FirstOrDefault()?.Paciente.Nome ?? "Nome não informado",
                    SobreNome = agendamento.AgendamentosPacientes.FirstOrDefault()?.Paciente.SobreNome ?? "SobreNome não informado",
                    DataDeNascimento = agendamento.AgendamentosPacientes.FirstOrDefault()?.Paciente.DataDeNascimento.ToString("dd/MM/yyyy") ?? "Data de Nascimento não informada",
                    Genero = agendamento.AgendamentosPacientes.FirstOrDefault()?.Paciente.Genero ?? "Genero não informado",
                    CPF = agendamento.AgendamentosPacientes.FirstOrDefault()?.Paciente.CPF ?? "CPF não informado",
                    Celular = agendamento.AgendamentosPacientes.FirstOrDefault()?.Paciente.Celular ?? "Celular não informado",
                    Email = agendamento.AgendamentosPacientes.FirstOrDefault()?.Paciente.Email ?? "Email não informado",
                    Profissao = agendamento.AgendamentosPacientes.FirstOrDefault()?.Paciente.Profissao ?? "Profissão não informado",
                },
                Funcionario = new FuncionarioDTO
                {
                    FuncionarioId = agendamento.Funcionario.FuncionarioId,
                    Nome = agendamento.Funcionario.Nome,
                    SobreNome = agendamento.Funcionario.SobreNome,
                    Idade = agendamento.Funcionario.Idade,
                    Especialidade = agendamento.Funcionario.Especialidade
                },
            };

            _logger.LogInformation($"Informações do agendamentoFuncionProcedimentosDTO: {JsonConvert.SerializeObject(agendamentoFuncionProcedimentosDTO)}");

            return agendamentoFuncionProcedimentosDTO;
        }


        public Agendamento CriarAgendamento(AgendamentoFuncionProcedimentosRegisterDTO agendamentoDTO)
        {
            var procedimentoExistente = _procedimentoRepository.GetById(agendamentoDTO.ProcedimentoId);
            var pacienteExistente = _pacienteRepository.GetById(agendamentoDTO.PacienteId);
            var funcionarioExistente = _funcionarioRepository.GetById(agendamentoDTO.FuncionarioId);

            // Verifique se o procedimento, funcionário existem no banco
            if (procedimentoExistente == null || funcionarioExistente == null)
                return null;

            Paciente newPaciente = null;

            if (pacienteExistente == null)
            {
                newPaciente = new Paciente
                {
                    Nome = agendamentoDTO.NomePaciente,
                    SobreNome = agendamentoDTO.SobreNome,
                    DataDeNascimento = agendamentoDTO.DataDeNascimento,
                    Genero = agendamentoDTO.Genero,
                    CPF = agendamentoDTO.CPF,
                    Celular = agendamentoDTO.Celular,
                    Email = agendamentoDTO.Email,
                    Profissao = agendamentoDTO.Profissao
                };

                _pacienteRepository.Add(newPaciente);
            }

            var dataHoraMarcada = agendamentoDTO.AgendamentoProcedimentoRegisterDTO.DataHoraMarcada;

            // Arredonde os minutos para o próximo intervalo de meia hora
            var minutos = dataHoraMarcada.Minute;
            var minutosArredondados = (minutos / 30) * 30;
            dataHoraMarcada = new DateTime(dataHoraMarcada.Year, dataHoraMarcada.Month, dataHoraMarcada.Day, dataHoraMarcada.Hour, minutosArredondados, 0);

            // Verifique se já existe um agendamento para o mesmo funcionário no mesmo horário
            while (_agendamentoRepository.Buscar(c => c.DataHoraMarcada == dataHoraMarcada && c.FuncionarioId == agendamentoDTO.FuncionarioId).Any())
            {
                // Se o horário estiver ocupado, ajuste para o próximo intervalo de meia hora
                dataHoraMarcada = dataHoraMarcada.AddMinutes(30);
            }

            var novoAgendamento = new Agendamento
            {
                DataHoraMarcada = dataHoraMarcada,
                Sessoes = agendamentoDTO.AgendamentoProcedimentoRegisterDTO.Sessoes,
                Observacao = agendamentoDTO.AgendamentoProcedimentoRegisterDTO.Observacao,
                Funcionario = funcionarioExistente,
            };

            var novoAgendamentosPacientes = new AgendamentosPacientes
            {
                Paciente = pacienteExistente != null ? pacienteExistente : newPaciente,
                Agendamento = novoAgendamento,
                DataHoraMarcada = dataHoraMarcada,
            };

            var novoProcedimentosAgendamentos = new ProcedimentoAgendamento
            {
                Procedimento = procedimentoExistente,
                Agendamento = novoAgendamento,
                DataHoraMarcada = dataHoraMarcada,
            };

            try
            {
                _agendamentoRepository.Add(novoAgendamento);
                _agendamentosPacientesRepository.Add(novoAgendamentosPacientes);
                _procedimentoAgendamentosRepository.Add(novoProcedimentosAgendamentos);

                _logger.LogInformation($"Agendamento criado: {novoAgendamento}");

                return novoAgendamento;
            }

            catch (Exception ex)
            {

                throw new Exception($"Erro ao criar o agendamento: {ex.Message}");
            }

        }

        public bool AtualizarAgendamento(int id, JsonPatchDocument<Agendamento> patchDocument)
        {
            Agendamento? agendamento = _agendamentoRepository.GetTodosAgendamentosById(id);

            try
            {
                if (agendamento == null)
                    return false;

                // Aplica as modificações contidas no JsonPatchDocument
                patchDocument.ApplyTo(agendamento);

                _agendamentoRepository.Update(agendamento);

                // Atualize a DataHoraMarcada nas tabelas AgendamentosPacientes e ProcedimentoAgendamentos
                _agendamentosPacientesRepository.UpdateDataHoraMarcada(id, agendamento.DataHoraMarcada);
                _procedimentoAgendamentosRepository.UpdateDataHoraMarcada(id, agendamento.DataHoraMarcada);

                return true;
            }
            catch (Exception ex)
            {
                throw new ServiceException("Falha ao atualizar o agendamento.", ex);
            }
        }

        public bool DeletarAgendamento(int id)
        {
            var resultado = _agendamentoRepository.GetTodosAgendamentosById(id);

            if (resultado == null)
                return false;

            _agendamentoRepository.Delete(resultado);
            return true;
        }

    } 
}

  

