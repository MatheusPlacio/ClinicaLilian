using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mapping
{
    public class ProcedimentoAgendamentoMapping : IEntityTypeConfiguration<ProcedimentoAgendamento>
    {
        public void Configure(EntityTypeBuilder<ProcedimentoAgendamento> builder)
        {
            builder.HasKey(x => x.ProcedimentoAgendamentoId);

            builder.Property(x => x.DataHoraMarcada).IsRequired();

            builder.HasOne(x => x.Procedimento).WithMany(x => x.ProcedimentoAgendamentos).HasForeignKey(x => x.ProcedimentoId);

            builder.HasOne(x => x.Agendamento).WithMany(x => x.ProcedimentoAgendamentos).HasForeignKey(x => x.AgendamentoId);
        }
    }
}
