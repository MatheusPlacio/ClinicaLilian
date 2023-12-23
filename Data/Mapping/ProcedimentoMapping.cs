using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiLF.Mapping
{
    public class ProcedimentoMapping : IEntityTypeConfiguration<Procedimento>
    {
        public void Configure(EntityTypeBuilder<Procedimento> builder)
        {
            builder.HasKey(x => x.ProcedimentoId);

            builder.HasData(
                new Procedimento
                {
                    ProcedimentoId = 1,
                    NomeProcedimento = "Limpeza de pele"
                },
                new Procedimento
                {
                    ProcedimentoId = 2,
                    NomeProcedimento = "Design de sobrancelhas"
                }, new Procedimento
                {
                    ProcedimentoId = 3,
                    NomeProcedimento = "Design de sobrancelha c/ henna ou tintura"
                },
                new Procedimento
                {
                    ProcedimentoId = 4,
                    NomeProcedimento = "Depilação a laser"
                },
                new Procedimento
                {
                    ProcedimentoId = 5,
                    NomeProcedimento = "RadioFrequencia"
                },
                new Procedimento
                {
                    ProcedimentoId = 6,
                    NomeProcedimento = "Pelling químico"
                },
                new Procedimento
                {
                    ProcedimentoId = 7,
                    NomeProcedimento = "Micropigmentação de sobrancelha"
                },
                new Procedimento
                {
                    ProcedimentoId = 8,
                    NomeProcedimento = "Preenchimento Labial"
                },
                new Procedimento
                {
                    ProcedimentoId = 9,
                    NomeProcedimento = "Botox"
                },
                new Procedimento
                {
                    ProcedimentoId = 10,
                    NomeProcedimento = "Bioestimulador"
                },
                new Procedimento
                {
                    ProcedimentoId = 11,
                    NomeProcedimento = "Bioestimulador"
                },
                new Procedimento
                {
                    ProcedimentoId = 12,
                    NomeProcedimento = "Aplicação de enzimas"
                }
              );
        }
    }
}
