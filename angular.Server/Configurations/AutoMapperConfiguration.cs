using Application.Wrappers;
using Microsoft.Extensions.DependencyInjection;

namespace IoC.Configurations;

public static class AutoMapperConfiguration
{
    public static IServiceCollection AddAutoMapperConfiguration(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(Response<string>).Assembly);

        return services;
    }
}