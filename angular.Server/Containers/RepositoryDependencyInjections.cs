using Repository.Repositories;
using webapi.Dao.Repositories;

namespace IoC.Containers;

public static class RepositoryDependencyInjections
{
    public static IServiceCollection AddRepositoryInjections(this IServiceCollection services)
    {
        services.AddScoped<IAccesoRepository, AccesoRepository>()
            .AddScoped<IAulaRepository, AulaRepository>()
            .AddScoped<IDimensionPsicologicaRepository, DimensionPsicologicaRepository>()
            .AddScoped<IDocenteRepository, DocenteRepository>()
            .AddScoped<IEscalaPsicologicaRepository, EscalaPsicologicaRepository>()
            .AddScoped<IEscuelaRepository, EscuelaRepository>()
            .AddScoped<IEstudianteRepository, EstudianteRepository>()
            .AddScoped<IEstudianteAulaRepository, EstudianteAulaRepository>()
            .AddScoped<IEvaluacionPsicologicaRepository, EvaluacionPsicologicaRepository>()
            .AddScoped<IEvaluacionPsicologicaAulaRepository, EvaluacionPsicologicaAulaRepository>()
            .AddScoped<IEvaluacionPsicologicaEstudianteRepository, EvaluacionPsicologicaEstudianteRepository>()
            .AddScoped<IGradoRepository, GradoRepository>()
            .AddScoped<IGradoEvaPsicologicaRepository, GradoEvaPsicologicaRepository>()
            .AddScoped<IIndicadorPsicologicoRepository, IndicadorPsicologicoRepository>()
            .AddScoped<INivelRepository, NivelRepository>()
            .AddScoped<IPersonaRepository, PersonaRepository>()
            .AddScoped<IPreguntaPsicologicaRepository, PreguntaPsicologicaRepository>()
            .AddScoped<IRespuestaPsicologicaRepository, RespuestaPsicologicaRepository>()
            .AddScoped<IRolRepository, RolRepository>()
            .AddScoped<IRolAccesoRepository, RolAccesoRepository>()
            .AddScoped<IRolUsuarioRepository, RolUsuarioRepository>()
            .AddScoped<IUnidadRepository, UnidadRepository>()
            .AddScoped<IUsuarioRepository, UsuarioRepository>();

        return services;
    }
}