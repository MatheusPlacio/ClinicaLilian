using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiLF.Mapping
{
    public class AgendamentoMapping : IEntityTypeConfiguration<Agendamento>
    {
        public void Configure(EntityTypeBuilder<Agendamento> builder)
        {
            builder.HasKey(x => x.AgendamentoId);
            builder.Property(x => x.AgendamentoId).ValueGeneratedOnAdd(); //Gera o id automatico

            builder.Property(x => x.Sessoes).IsRequired();

            builder.Property(x => x.Observacao).IsRequired().HasMaxLength(150); ;

            builder.Property(x => x.DataHoraMarcada).IsRequired();
        }
    }
}
