using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiLF.Mapping
{
    public class FuncionarioMapping : IEntityTypeConfiguration<Funcionario>
    {
        public void Configure(EntityTypeBuilder<Funcionario> builder)
        {
            builder.HasKey(x => x.FuncionarioId);
            builder.Property(x => x.FuncionarioId).ValueGeneratedOnAdd(); //Gera o id automatico

            builder.Property(x => x.Nome).IsRequired().HasMaxLength(20);
            builder.Property(x => x.SobreNome).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Idade).IsRequired().HasMaxLength(2);

            builder.Property(x => x.Celular).IsRequired().HasMaxLength(14); // Celular é obrigatório e pode conter no máximo 14 caracteres.

            builder.Property(x => x.CPF).IsRequired();

            builder.HasIndex(x => x.CPF);

            builder.Property(x => x.Email).HasMaxLength(70); // Email é opcional e pode conter até 70 caracteres

            builder.Property(x => x.DataNascimento)
                  .IsRequired()
                  .HasColumnType("date")
                  .HasDefaultValueSql("GETDATE()"); // Data de Nascimento obrigatoria, definindo no banco como date retornando a data.

            builder.Property(x => x.Especialidade).IsRequired().HasMaxLength(20);


            //EF
            builder.HasMany(x => x.Agendamento).WithOne(x => x.Funcionario).HasForeignKey(x => x.FuncionarioId);
        }
    }
}
