using eCommerce.User.Core.Services;
using eCommerce.User.Core.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerce.User.Core;

public static class RegisterCoreServices
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        // Register core services here
        services.AddTransient<IUserService, UserService>();
        services.AddValidatorsFromAssemblyContaining<RegisterValidator>();
        services.AddValidatorsFromAssemblyContaining<LoginValidator>();

        return services;
    }
}
