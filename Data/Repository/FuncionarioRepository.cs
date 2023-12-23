using Data.Context;
using Domain.Interfaces.IRepository;
using Domain.Models;

namespace Data.Repository
{
    public class FuncionarioRepository : Repository<Funcionario>, IFuncionarioRepository
    {
        public FuncionarioRepository(MyContext context) : base(context)
        {
            
        }
    }
}
