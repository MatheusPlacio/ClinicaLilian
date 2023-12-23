using ApiLF.Mapping;
using Data.Mapping;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class MyContext : DbContext
    {
        public DbSet<Agendamento> Agendamentos { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Procedimento> Procedimentos { get; set; }
        public DbSet<ProcedimentoAgendamento> ProcedimentosAgendamentos { get; set; }
        public DbSet<AgendamentosPacientes> AgendamentosPacientes { get; set; }

        public MyContext(DbContextOptions<MyContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new AgendamentoMapping());
            modelBuilder.ApplyConfiguration(new EnderecoMapping());
            modelBuilder.ApplyConfiguration(new FuncionarioMapping());
            modelBuilder.ApplyConfiguration(new PacienteMapping());
            modelBuilder.ApplyConfiguration(new ProcedimentoMapping());
            modelBuilder.ApplyConfiguration(new ProcedimentoAgendamentoMapping());
            modelBuilder.ApplyConfiguration(new AgendamentosPacientesMapping());
        }
    }
}
