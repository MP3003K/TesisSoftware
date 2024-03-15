namespace Configurations
{
    public static class Configurations
    {
        public static IServiceCollection AddConfigurations(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            services.AddDatabaseConfiguration(configuration)
                .AddMediatRConfiguration()
                .AddAutoMapperConfiguration();

            return services;
        }
    }
}

