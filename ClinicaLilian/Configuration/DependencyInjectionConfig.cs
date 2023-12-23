using Data.Repository;
using Domain.Interfaces.IRepository;
using Domain.Interfaces.IService;
using Service.Services;

namespace ClinicaLilian.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            //Repository
            services.AddScoped<IAgendamentoRepository, AgendamentoRepository>();
            services.AddScoped<IPacienteRepository, PacienteRepository>();
            services.AddScoped<IEnderecoRepository, EnderecoRepository>();
            services.AddScoped<IFuncionarioRepository, FuncionarioRepository>();
            services.AddScoped<IProcedimentoRepository, ProcedimentoRepository>();
            services.AddScoped<IProcedimentoAgendamentosRepository, ProcedimentoAgendamentosRepository>();
            services.AddScoped<IAgendamentosPacientesRepository, AgendamentosPacientesRepository>();

            //Service
            services.AddScoped<IAgendamentoService, AgendamentoService>();
            services.AddScoped<IPacienteService, PacienteService>();
            services.AddScoped<IEnderecoService, EnderecoService>();
            services.AddScoped<IFuncionarioService, FuncionarioService>();
            services.AddScoped<IProcedimentoService, ProcedimentoService>();
            services.AddScoped<IAgendamentosPacientesService, AgendamentosPacientesService>();
            services.AddScoped<IProcedimentoAgendamentoService, ProcedimentoAgendamentoService>();
        }
    }
}
