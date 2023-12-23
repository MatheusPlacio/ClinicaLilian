using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mapping
{
    public class AgendamentosPacientesMapping : IEntityTypeConfiguration<AgendamentosPacientes>
    {
        public void Configure(EntityTypeBuilder<AgendamentosPacientes> builder)
        {
            builder.HasKey(x => x.AgendamentosPacientesId);

            builder.Property(x => x.DataHoraMarcada).IsRequired();

            builder.HasOne(ap => ap.Agendamento).WithMany(pa => pa.AgendamentosPacientes).HasForeignKey(x => x.AgendamentoId);

            builder.HasOne(ap => ap.Paciente).WithMany(p => p.AgendamentosPacientes).HasForeignKey(ap => ap.PacienteId);
        }
    }
}
