using Contracts.Transactions;
using Microsoft.Extensions.DependencyInjection;
using Repository.Transaction;

namespace IoC.Containers;

public static class DependencyInjections
{
    public static IServiceCollection AddInjections(this IServiceCollection services)
    {
        services.AddScoped<IDatabaseTransaction, DatabaseTransaction>()
            .AddRepositoryInjections()
            .AddServiceInjections();

        return services;
    }
}