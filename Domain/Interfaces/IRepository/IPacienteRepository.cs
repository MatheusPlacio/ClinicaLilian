using Domain.Models;

namespace Domain.Interfaces.IRepository
{
    public interface IPacienteRepository : IRepository<Paciente>
    {
        IList<Paciente> GetTodosPacientesEnderecos();
    }
}
