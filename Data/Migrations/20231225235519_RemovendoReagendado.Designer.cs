﻿// <auto-generated />
using System;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Data.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20231225235519_RemovendoReagendado")]
    partial class RemovendoReagendado
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Domain.Models.Agendamento", b =>
                {
                    b.Property<int>("AgendamentoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AgendamentoId"), 1L, 1);

                    b.Property<bool>("Cancelado")
                        .HasColumnType("bit");

                    b.Property<DateTime>("DataHoraMarcada")
                        .HasColumnType("datetime2");

                    b.Property<int>("FuncionarioId")
                        .HasColumnType("int");

                    b.Property<string>("Observacao")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<bool>("Realizado")
                        .HasColumnType("bit");

                    b.Property<int>("Sessoes")
                        .HasColumnType("int");

                    b.HasKey("AgendamentoId");

                    b.HasIndex("FuncionarioId");

                    b.ToTable("Agendamentos");
                });

            modelBuilder.Entity("Domain.Models.AgendamentosPacientes", b =>
                {
                    b.Property<int>("AgendamentosPacientesId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AgendamentosPacientesId"), 1L, 1);

                    b.Property<int>("AgendamentoId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataHoraMarcada")
                        .HasColumnType("datetime2");

                    b.Property<int>("PacienteId")
                        .HasColumnType("int");

                    b.HasKey("AgendamentosPacientesId");

                    b.HasIndex("AgendamentoId");

                    b.HasIndex("PacienteId");

                    b.ToTable("AgendamentosPacientes");
                });

            modelBuilder.Entity("Domain.Models.Endereco", b =>
                {
                    b.Property<int>("EnderecoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EnderecoId"), 1L, 1);

                    b.Property<string>("Bairro")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Cep")
                        .IsRequired()
                        .HasMaxLength(9)
                        .HasColumnType("nvarchar(9)");

                    b.Property<string>("Complemento")
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

                    b.Property<string>("Logradouro")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

                    b.Property<int>("Numero")
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    b.Property<string>("UF")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("nvarchar(2)");

                    b.Property<string>("localidade")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

                    b.HasKey("EnderecoId");

                    b.ToTable("Enderecos");
                });

            modelBuilder.Entity("Domain.Models.Funcionario", b =>
                {
                    b.Property<int>("FuncionarioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FuncionarioId"), 1L, 1);

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<string>("Celular")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)");

                    b.Property<DateTime>("DataNascimento")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("date")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

                    b.Property<string>("Especialidade")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("Idade")
                        .HasMaxLength(2)
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("SobreNome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("FuncionarioId");

                    b.HasIndex("CPF");

                    b.ToTable("Funcionarios");
                });

            modelBuilder.Entity("Domain.Models.Paciente", b =>
                {
                    b.Property<int>("PacienteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PacienteId"), 1L, 1);

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<string>("Celular")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)");

                    b.Property<DateTime>("DataDeNascimento")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("date")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

                    b.Property<string>("Genero")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int?>("Idade")
                        .HasMaxLength(2)
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Profissao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SobreNome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("PacienteId");

                    b.HasIndex("CPF")
                        .IsUnique();

                    b.HasIndex("Celular")
                        .IsUnique();

                    b.ToTable("Pacientes");
                });

            modelBuilder.Entity("Domain.Models.Procedimento", b =>
                {
                    b.Property<int>("ProcedimentoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProcedimentoId"), 1L, 1);

                    b.Property<string>("NomeProcedimento")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProcedimentoId");

                    b.ToTable("Procedimentos");

                    b.HasData(
                        new
                        {
                            ProcedimentoId = 1,
                            NomeProcedimento = "Limpeza de pele"
                        },
                        new
                        {
                            ProcedimentoId = 2,
                            NomeProcedimento = "Design de sobrancelhas"
                        },
                        new
                        {
                            ProcedimentoId = 3,
                            NomeProcedimento = "Design de sobrancelha c/ henna ou tintura"
                        },
                        new
                        {
                            ProcedimentoId = 4,
                            NomeProcedimento = "Depilação a laser"
                        },
                        new
                        {
                            ProcedimentoId = 5,
                            NomeProcedimento = "RadioFrequencia"
                        },
                        new
                        {
                            ProcedimentoId = 6,
                            NomeProcedimento = "Pelling químico"
                        },
                        new
                        {
                            ProcedimentoId = 7,
                            NomeProcedimento = "Micropigmentação de sobrancelha"
                        },
                        new
                        {
                            ProcedimentoId = 8,
                            NomeProcedimento = "Preenchimento Labial"
                        },
                        new
                        {
                            ProcedimentoId = 9,
                            NomeProcedimento = "Botox"
                        },
                        new
                        {
                            ProcedimentoId = 10,
                            NomeProcedimento = "Bioestimulador"
                        },
                        new
                        {
                            ProcedimentoId = 11,
                            NomeProcedimento = "Bioestimulador"
                        },
                        new
                        {
                            ProcedimentoId = 12,
                            NomeProcedimento = "Aplicação de enzimas"
                        });
                });

            modelBuilder.Entity("Domain.Models.ProcedimentoAgendamento", b =>
                {
                    b.Property<int>("ProcedimentoAgendamentoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProcedimentoAgendamentoId"), 1L, 1);

                    b.Property<int>("AgendamentoId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataHoraMarcada")
                        .HasColumnType("datetime2");

                    b.Property<int>("ProcedimentoId")
                        .HasColumnType("int");

                    b.HasKey("ProcedimentoAgendamentoId");

                    b.HasIndex("AgendamentoId");

                    b.HasIndex("ProcedimentoId");

                    b.ToTable("ProcedimentosAgendamentos");
                });

            modelBuilder.Entity("Domain.Models.Agendamento", b =>
                {
                    b.HasOne("Domain.Models.Funcionario", "Funcionario")
                        .WithMany("Agendamento")
                        .HasForeignKey("FuncionarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Funcionario");
                });

            modelBuilder.Entity("Domain.Models.AgendamentosPacientes", b =>
                {
                    b.HasOne("Domain.Models.Agendamento", "Agendamento")
                        .WithMany("AgendamentosPacientes")
                        .HasForeignKey("AgendamentoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.Paciente", "Paciente")
                        .WithMany("AgendamentosPacientes")
                        .HasForeignKey("PacienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Agendamento");

                    b.Navigation("Paciente");
                });

            modelBuilder.Entity("Domain.Models.ProcedimentoAgendamento", b =>
                {
                    b.HasOne("Domain.Models.Agendamento", "Agendamento")
                        .WithMany("ProcedimentoAgendamentos")
                        .HasForeignKey("AgendamentoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.Procedimento", "Procedimento")
                        .WithMany("ProcedimentoAgendamentos")
                        .HasForeignKey("ProcedimentoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Agendamento");

                    b.Navigation("Procedimento");
                });

            modelBuilder.Entity("Domain.Models.Agendamento", b =>
                {
                    b.Navigation("AgendamentosPacientes");

                    b.Navigation("ProcedimentoAgendamentos");
                });

            modelBuilder.Entity("Domain.Models.Funcionario", b =>
                {
                    b.Navigation("Agendamento");
                });

            modelBuilder.Entity("Domain.Models.Paciente", b =>
                {
                    b.Navigation("AgendamentosPacientes");
                });

            modelBuilder.Entity("Domain.Models.Procedimento", b =>
                {
                    b.Navigation("ProcedimentoAgendamentos");
                });
#pragma warning restore 612, 618
        }
    }
}
