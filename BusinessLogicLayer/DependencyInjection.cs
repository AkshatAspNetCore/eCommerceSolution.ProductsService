using BusinessLogicLayer.Mappers;
using BusinessLogicLayer.ServiceContracts;
using BusinessLogicLayer.Services;
using BusinessLogicLayer.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLogicLayer;

public static class DependencyInjection
{
    public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services)
    {

        // Add Business Logic Layer services into the IoC container

        services.AddAutoMapper(config =>
        {
            config.AddProfile<ProductAddRequestToProductMappingProfile>();
            config.AddProfile<ProductToProductResponseMappingProfile>();
            config.AddProfile<ProductUpdateRequestToProductMappingProfile>();
        });
        services.AddScoped<IProductService, ProductsService>();
        services.AddValidatorsFromAssemblyContaining<ProductAddRequestValidator>();
        return services;
    }
}

