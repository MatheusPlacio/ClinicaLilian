using Domain.DTOs.FuncionariosDTO;
using Domain.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace Domain.Interfaces.IService
{
    public interface IFuncionarioService
    {
        IList<FuncionarioDTO> ObterTodosFuncionarios();
        FuncionarioDTO ObterFuncionarioPorId(int id);
        void CriarFuncionario(Funcionario funcionario);
        bool AtualizarFuncionario(FuncionarioDTO funcionarioDTO);
        bool AtualizarFuncionarioParcial(int id, JsonPatchDocument<Funcionario> patchDocument);
        bool DeletarFuncionario(int funcionarioId);

    }
}
