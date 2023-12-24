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

        public IList<Paciente> GetPacienteCpf(string cpf)
        {
            var resultado = _context.Pacientes.Where(x => x.CPF == cpf).ToList();

            return resultado;
        }
    }
}
