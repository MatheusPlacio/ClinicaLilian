using Data.Context;
using Domain.Interfaces.IRepository;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository
{
    public class PacienteRepository : Repository<Paciente>, IPacienteRepository
    {
        private readonly MyContext _context;
        public PacienteRepository(MyContext context) : base(context)
        {
            _context = context;
        }

        public IList<Paciente> GetTodosPacientesEnderecos()
        {
            var resultado =  _context.Pacientes.Include(x => x.Endereco).ToList();

            return resultado;
        }
    }
}
