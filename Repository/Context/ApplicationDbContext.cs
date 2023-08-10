using System.Diagnostics.CodeAnalysis;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Repository.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext([NotNull] DbContextOptions options, IConfiguration configuration) : base(options)
    {
    }

    public DbSet<Bank> Banks { get; private init; }
    public DbSet<Pix> Pixes { get; private init; }
    public DbSet<Domain.Entities.Transaction> Transactions { get; private init; }
    public DbSet<Aula> Aulas { get; private init; }
    public DbSet<DimensionPsicologica> Dimensiones { get; private init; }
    public DbSet<Docente> Docentes { get; private init; }
    public DbSet<EscalaPsicologica> Escalas { get; private init; }
    public DbSet<Escuela> Escuelas { get; private init; }
    public DbSet<Estudiante> Estudiantes { get; private init; }
    public DbSet<EvaluacionPsicologicaAula> EvalucionAulas { get; private init; }
    public DbSet<EvaluacionPsicologicaEstudiante> EvaluacionEstudiantes { get; private init; }
    public DbSet<Grado> Grados { get; private init; }
    public DbSet<IndicadorPsicologico> Indicadores { get; private init; }
    public DbSet<Nivel> Niveles { get; private init; }
    public DbSet<Persona> Personas { get; private init; }
    public DbSet<EvaluacionPsicologica> EvaluacionPsicologica { get; private init; }
    public DbSet<Unidad> Unidades { get; private init; }
    public DbSet<Usuario> Usuarios { get; private init; }
    public DbSet<RespuestaPsicologica> RespuestaPsicologicas { get; private init; }
    public DbSet<PreguntaPsicologica> PreguntaPsicologicas { get; private init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}