using Domain.DTOs.PacientesDTO;
using Domain.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace Domain.Interfaces.IService
{
    public interface IPacienteService
    {
        IList<PacienteDTO> ObterTodosPacientes();
        PacienteDTO? ObterPacientePorId(int id);
        void CriarPaciente(Paciente paciente);
        bool AtualizarPaciente(PacienteUpdateDTO pacienteDTO);
        bool AtualizarPacienteParcial(int id, JsonPatchDocument<Paciente> patchDocument);
        bool DeletarPaciente(int pacienteId);
        IList<PacienteEnderecoDTO> GetTodosPacientesEnderecos();
    }
}
