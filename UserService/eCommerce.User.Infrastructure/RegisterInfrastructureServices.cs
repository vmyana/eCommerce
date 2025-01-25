using eCommerce.User.Core.Repositories;
using eCommerce.User.Infrastructure.DbContext;
using eCommerce.User.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerce.User.Infrastructure;

public static class RegisterInfrastructureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        // Register infrastructure services here
        services.AddTransient<DapperDbContext>();
        services.AddTransient<IUserRepository, UserRepository>();
        return services;
    }
}
