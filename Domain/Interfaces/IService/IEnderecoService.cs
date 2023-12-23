using Domain.DTOs.EnderecoDTO;

namespace Domain.Interfaces.IService
{
    public interface IEnderecoService
    {
        IList<EnderecoGetDTO> GetTodosEnderecos();
        void AdicionarEndereco(EnderecoCepDTO cep);
        EnderecoDTO ObterEnderecoPorId(int id);
        bool AtualizarEndereco(EnderecoDTO endereco);
        bool DeletarEndereco(int id);
    }
}
