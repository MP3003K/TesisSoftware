

using Interfaces.Transactions;
using Repository.Transaction;

namespace Containers
{
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
}

