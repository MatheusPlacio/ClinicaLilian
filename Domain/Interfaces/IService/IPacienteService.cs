using Domain.DTOs.PacientesDTO;
using Domain.Models;
using Domain.Settings;
using Microsoft.AspNetCore.JsonPatch;

namespace Domain.Interfaces.IService
{
    public interface IPacienteService
    {
        IList<PacienteDTO> ObterTodosPacientes();
        PacienteDTO? ObterPacientePorId(int id);
        ServiceResult CriarPaciente(Paciente paciente);
        ServiceResult AtualizarPaciente(PacienteUpdateDTO pacienteDTO);
        bool AtualizarPacienteParcial(int id, JsonPatchDocument<Paciente> patchDocument);
        bool DeletarPaciente(int pacienteId);
        IList<PacienteDTO> ObterPacientePeloCPF(string cpf);
        //IList<PacienteEnderecoDTO> GetTodosPacientesEnderecos();
    }
}
