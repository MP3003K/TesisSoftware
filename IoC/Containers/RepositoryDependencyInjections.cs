using Contracts.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Repository.Repositories;

namespace IoC.Containers;

public static class RepositoryDependencyInjections
{
    public static IServiceCollection AddRepositoryInjections(this IServiceCollection services)
    {
        services.AddScoped<IUnidadRepository, UnidadRepository>()
            .AddScoped<IPreguntaPsicologicaRepository, PreguntaPsicologicaRepository>();
        return services;
    }
}