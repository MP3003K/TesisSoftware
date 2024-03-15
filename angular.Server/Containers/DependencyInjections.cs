using Repository.Transaction;
using webapi.Dao.Transactions;

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