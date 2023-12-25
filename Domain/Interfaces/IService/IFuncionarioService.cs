using Domain.DTOs.FuncionariosDTO;
using Domain.Models;
using Domain.Settings;
using Microsoft.AspNetCore.JsonPatch;

namespace Domain.Interfaces.IService
{
    public interface IFuncionarioService
    {
        IList<FuncionarioDTO> ObterTodosFuncionarios();
        FuncionarioDTO ObterFuncionarioPorId(int id);
        ServiceResult CriarFuncionario(Funcionario funcionario);
        ServiceResult AtualizarFuncionario(FuncionarioDTO funcionarioDTO);
        bool AtualizarFuncionarioParcial(int id, JsonPatchDocument<Funcionario> patchDocument);
        bool DeletarFuncionario(int funcionarioId);

    }
}
