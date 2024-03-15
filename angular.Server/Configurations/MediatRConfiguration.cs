using Application.Wrappers;

namespace IoC.Configurations
{
    public static class MediatRConfiguration
    {
        public static IServiceCollection AddMediatRConfiguration(this IServiceCollection services)
        {
            services.AddMediatR(
            cfg => cfg.RegisterServicesFromAssembly(typeof(Response<string>).Assembly)
            );

            return services;
        }
    }
}
