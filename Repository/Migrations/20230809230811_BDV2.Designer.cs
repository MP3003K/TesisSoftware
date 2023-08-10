﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Repository.Context;

#nullable disable

namespace Repository.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230809230811_BDV2")]
    partial class BDV2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Aula", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("DocenteId")
                        .HasColumnType("integer");

                    b.Property<int>("EscuelaId")
                        .HasColumnType("integer");

                    b.Property<int>("GradoId")
                        .HasColumnType("integer");

                    b.Property<int>("Secccion")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("DocenteId");

                    b.HasIndex("EscuelaId");

                    b.HasIndex("GradoId");

                    b.ToTable("Aulas", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Bank", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Banks", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.DimensionPsicologica", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("DimensionesPsicologicas", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Docente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("PersonaId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PersonaId")
                        .IsUnique();

                    b.ToTable("Docentes", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.EscalaPsicologica", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("DimensionId")
                        .HasColumnType("integer");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("DimensionId");

                    b.ToTable("EscalasPsicologicas", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Escuela", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("CodigoModular")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Escuelas", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Estudiante", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AulaId")
                        .HasColumnType("integer");

                    b.Property<string>("CodigoEstudiante")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PersonaId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AulaId");

                    b.HasIndex("PersonaId")
                        .IsUnique();

                    b.ToTable("Estudiantes", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.EvaluacionPsicologica", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("PruebasPsicologicas", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.EvaluacionPsicologicaAula", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AulaId")
                        .HasColumnType("integer");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("EvaluacionPsicologicaId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("FechaFin")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("UnidadId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AulaId");

                    b.HasIndex("EvaluacionPsicologicaId");

                    b.HasIndex("UnidadId");

                    b.ToTable("EvaluacionesAula", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.EvaluacionPsicologicaEstudiante", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("EstudianteId")
                        .HasColumnType("integer");

                    b.Property<int>("EvaluacionAulaId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("FechaFin")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("RespuestaPsicologicaId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("EstudianteId");

                    b.HasIndex("EvaluacionAulaId");

                    b.ToTable("EvaluacionesEstudiante", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Grado", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("NGrado")
                        .HasColumnType("integer");

                    b.Property<int>("NivelId")
                        .HasColumnType("integer");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("NivelId");

                    b.ToTable("Grados", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.IndicadorPsicologico", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("EscalaId")
                        .HasColumnType("integer");

                    b.Property<int?>("EscalaId1")
                        .HasColumnType("integer");

                    b.Property<string>("NombreIndicador")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("EscalaId");

                    b.HasIndex("EscalaId1");

                    b.ToTable("IndicadoresPsicologicos", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Nivel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Niveles", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Persona", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ApellidoMaterno")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ApellidoPaterno")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("DNI")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Nombres")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Personas", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Pix", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("BankId")
                        .HasColumnType("integer");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("BankId");

                    b.ToTable("Pixes", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.PreguntaPsicologica", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("IndicadorPsicologicoId")
                        .HasColumnType("integer");

                    b.Property<string>("Pregunta")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("IndicadorPsicologicoId");

                    b.ToTable("PreguntasPsicologicas", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.RespuestaPsicologica", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("EvaPsiEstId")
                        .HasColumnType("integer");

                    b.Property<int>("PreguntaPsicologicaId")
                        .HasColumnType("integer");

                    b.Property<int>("Puntaje")
                        .HasColumnType("integer");

                    b.Property<string>("Respuesta")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("EvaPsiEstId");

                    b.HasIndex("PreguntaPsicologicaId");

                    b.ToTable("RespuestasPsicologicas", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<int>("PixId")
                        .HasColumnType("integer");

                    b.Property<double>("Value")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("PixId");

                    b.ToTable("Transactions", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Unidad", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("EscuelaId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("FechaFin")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("NUnidad")
                        .HasColumnType("integer");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("EscuelaId");

                    b.ToTable("Unidades", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PersonaId")
                        .HasColumnType("integer");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("PersonaId")
                        .IsUnique();

                    b.ToTable("Usuarios", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Aula", b =>
                {
                    b.HasOne("Domain.Entities.Docente", null)
                        .WithMany("Aulas")
                        .HasForeignKey("DocenteId");

                    b.HasOne("Domain.Entities.Escuela", "Escuela")
                        .WithMany("Aulas")
                        .HasForeignKey("EscuelaId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Grado", "Grado")
                        .WithMany("Aulas")
                        .HasForeignKey("GradoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Escuela");

                    b.Navigation("Grado");
                });

            modelBuilder.Entity("Domain.Entities.Docente", b =>
                {
                    b.HasOne("Domain.Entities.Persona", "Persona")
                        .WithOne("Docente")
                        .HasForeignKey("Domain.Entities.Docente", "PersonaId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Persona");
                });

            modelBuilder.Entity("Domain.Entities.EscalaPsicologica", b =>
                {
                    b.HasOne("Domain.Entities.DimensionPsicologica", "DimensionPsicologica")
                        .WithMany("EscalasPsicologicas")
                        .HasForeignKey("DimensionId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("DimensionPsicologica");
                });

            modelBuilder.Entity("Domain.Entities.Estudiante", b =>
                {
                    b.HasOne("Domain.Entities.Aula", "Aula")
                        .WithMany("Estudiantes")
                        .HasForeignKey("AulaId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Persona", "Persona")
                        .WithOne("Estudiante")
                        .HasForeignKey("Domain.Entities.Estudiante", "PersonaId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Aula");

                    b.Navigation("Persona");
                });

            modelBuilder.Entity("Domain.Entities.EvaluacionPsicologicaAula", b =>
                {
                    b.HasOne("Domain.Entities.Aula", "Aula")
                        .WithMany("EvaluacionesPsicologicasAula")
                        .HasForeignKey("AulaId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Domain.Entities.EvaluacionPsicologica", "EvaluacionPsicologica")
                        .WithMany("EvaluacionesPsicologicasAula")
                        .HasForeignKey("EvaluacionPsicologicaId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Unidad", "Unidad")
                        .WithMany("EvaluacionesPsicologicasAula")
                        .HasForeignKey("UnidadId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Aula");

                    b.Navigation("EvaluacionPsicologica");

                    b.Navigation("Unidad");
                });

            modelBuilder.Entity("Domain.Entities.EvaluacionPsicologicaEstudiante", b =>
                {
                    b.HasOne("Domain.Entities.Estudiante", "Estudiante")
                        .WithMany("EvaluacionesEstudiante")
                        .HasForeignKey("EstudianteId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Domain.Entities.EvaluacionPsicologicaAula", "EvaluacionAula")
                        .WithMany("EvaluacionesPsicologicasEstudiante")
                        .HasForeignKey("EvaluacionAulaId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Estudiante");

                    b.Navigation("EvaluacionAula");
                });

            modelBuilder.Entity("Domain.Entities.Grado", b =>
                {
                    b.HasOne("Domain.Entities.Nivel", "Nivel")
                        .WithMany("Grados")
                        .HasForeignKey("NivelId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Nivel");
                });

            modelBuilder.Entity("Domain.Entities.IndicadorPsicologico", b =>
                {
                    b.HasOne("Domain.Entities.EscalaPsicologica", "EscalaPsicologica")
                        .WithMany("IndicadoresPsicologicos")
                        .HasForeignKey("EscalaId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Domain.Entities.EscalaPsicologica", "Escala")
                        .WithMany()
                        .HasForeignKey("EscalaId1");

                    b.Navigation("Escala");

                    b.Navigation("EscalaPsicologica");
                });

            modelBuilder.Entity("Domain.Entities.Pix", b =>
                {
                    b.HasOne("Domain.Entities.Bank", "Bank")
                        .WithMany("Pixes")
                        .HasForeignKey("BankId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Bank");
                });

            modelBuilder.Entity("Domain.Entities.PreguntaPsicologica", b =>
                {
                    b.HasOne("Domain.Entities.IndicadorPsicologico", "IndicadorPsicologico")
                        .WithMany("PreguntasPsicologicas")
                        .HasForeignKey("IndicadorPsicologicoId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("IndicadorPsicologico");
                });

            modelBuilder.Entity("Domain.Entities.RespuestaPsicologica", b =>
                {
                    b.HasOne("Domain.Entities.EvaluacionPsicologicaEstudiante", "EvaluacionPsicologicaEstudiante")
                        .WithMany("RespuestasPsicologicas")
                        .HasForeignKey("EvaPsiEstId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Domain.Entities.PreguntaPsicologica", "PreguntaPsicologica")
                        .WithMany("RespuestasPsicologicas")
                        .HasForeignKey("PreguntaPsicologicaId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("EvaluacionPsicologicaEstudiante");

                    b.Navigation("PreguntaPsicologica");
                });

            modelBuilder.Entity("Domain.Entities.Transaction", b =>
                {
                    b.HasOne("Domain.Entities.Pix", "Pix")
                        .WithMany("Transactions")
                        .HasForeignKey("PixId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Pix");
                });

            modelBuilder.Entity("Domain.Entities.Unidad", b =>
                {
                    b.HasOne("Domain.Entities.Escuela", "Escuela")
                        .WithMany("Unidades")
                        .HasForeignKey("EscuelaId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Escuela");
                });

            modelBuilder.Entity("Domain.Entities.Usuario", b =>
                {
                    b.HasOne("Domain.Entities.Persona", "Persona")
                        .WithOne("Usuario")
                        .HasForeignKey("Domain.Entities.Usuario", "PersonaId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Persona");
                });

            modelBuilder.Entity("Domain.Entities.Aula", b =>
                {
                    b.Navigation("Estudiantes");

                    b.Navigation("EvaluacionesPsicologicasAula");
                });

            modelBuilder.Entity("Domain.Entities.Bank", b =>
                {
                    b.Navigation("Pixes");
                });

            modelBuilder.Entity("Domain.Entities.DimensionPsicologica", b =>
                {
                    b.Navigation("EscalasPsicologicas");
                });

            modelBuilder.Entity("Domain.Entities.Docente", b =>
                {
                    b.Navigation("Aulas");
                });

            modelBuilder.Entity("Domain.Entities.EscalaPsicologica", b =>
                {
                    b.Navigation("IndicadoresPsicologicos");
                });

            modelBuilder.Entity("Domain.Entities.Escuela", b =>
                {
                    b.Navigation("Aulas");

                    b.Navigation("Unidades");
                });

            modelBuilder.Entity("Domain.Entities.Estudiante", b =>
                {
                    b.Navigation("EvaluacionesEstudiante");
                });

            modelBuilder.Entity("Domain.Entities.EvaluacionPsicologica", b =>
                {
                    b.Navigation("EvaluacionesPsicologicasAula");
                });

            modelBuilder.Entity("Domain.Entities.EvaluacionPsicologicaAula", b =>
                {
                    b.Navigation("EvaluacionesPsicologicasEstudiante");
                });

            modelBuilder.Entity("Domain.Entities.EvaluacionPsicologicaEstudiante", b =>
                {
                    b.Navigation("RespuestasPsicologicas");
                });

            modelBuilder.Entity("Domain.Entities.Grado", b =>
                {
                    b.Navigation("Aulas");
                });

            modelBuilder.Entity("Domain.Entities.IndicadorPsicologico", b =>
                {
                    b.Navigation("PreguntasPsicologicas");
                });

            modelBuilder.Entity("Domain.Entities.Nivel", b =>
                {
                    b.Navigation("Grados");
                });

            modelBuilder.Entity("Domain.Entities.Persona", b =>
                {
                    b.Navigation("Docente");

                    b.Navigation("Estudiante");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Domain.Entities.Pix", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("Domain.Entities.PreguntaPsicologica", b =>
                {
                    b.Navigation("RespuestasPsicologicas");
                });

            modelBuilder.Entity("Domain.Entities.Unidad", b =>
                {
                    b.Navigation("EvaluacionesPsicologicasAula");
                });
#pragma warning restore 612, 618
        }
    }
}
