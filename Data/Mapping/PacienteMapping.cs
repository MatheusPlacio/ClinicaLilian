using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiLF.Mapping
{
    public class PacienteMapping : IEntityTypeConfiguration<Paciente>
    {
        public void Configure(EntityTypeBuilder<Paciente> builder)
        {
            builder.HasKey(x => x.PacienteId);
            builder.Property(x => x.PacienteId).ValueGeneratedOnAdd(); //Gera o id automatico

            builder.Property(x => x.Nome).IsRequired().HasMaxLength(30); // Nome é obrigatório e pode conter no máximo 30 caracteres.

            builder.HasIndex(x => x.Celular).IsUnique();
            builder.Property(x => x.Celular).IsRequired().HasMaxLength(14); // Celular é obrigatório e pode conter no máximo 14 caracteres.

            builder.Property(x => x.SobreNome).IsRequired().HasMaxLength(100); // sobreNome é obrigatório e pode conter no máximo 100 caracteres.

            builder.Property(x => x.Genero).IsRequired().HasMaxLength(30); // Genero é obrigatório e pode conter no máximo 30 caracteres.

            builder.Property(x => x.Idade).HasMaxLength(2); 

            builder.Property(x => x.DataDeNascimento)
                   .IsRequired()
                   .HasColumnType("date")
                   .HasDefaultValueSql("GETDATE()"); // Data de Nascimento obrigatoria, definindo no banco como date retornando a data.

            builder.Property(x => x.CPF).IsRequired();

            builder.HasIndex(x => x.CPF)
                .IsUnique(); // Define o índice único para o CPF

            builder.Property(x => x.Email).HasMaxLength(70); // Email é opcional e pode conter até 70 caracteres
        }
    }
}
