using Application.Wrappers;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace IoC.Configurations;

public static class MediatRConfiguration
{
    public static IServiceCollection AddMediatRConfiguration(this IServiceCollection services)
    {
        services.AddMediatR(typeof(Response<string>).Assembly);

        return services;
    }
}