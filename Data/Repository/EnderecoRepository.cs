using Data.Context;
using Domain.Interfaces.IRepository;
using Domain.Models;

namespace Data.Repository
{
    public class EnderecoRepository : Repository<Endereco>, IEnderecoRepository
    {
        public EnderecoRepository(MyContext context) : base(context)
        {
            
        }
    }
}
