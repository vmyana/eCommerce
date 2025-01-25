using eCommerce.Product.Core.Mappers;
using eCommerce.Product.Core.Services;
using eCommerce.Product.Core.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerce.Product.Core;

public static class RegisterCoreServices
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        // TODO: Register core services here
        services.AddAutoMapper(typeof(ProductMappingProfile).Assembly);
        services.AddValidatorsFromAssemblyContaining<ProductAddValidator>();
        services.AddTransient<IProductService, ProductService>();
        return services;
    }
}
