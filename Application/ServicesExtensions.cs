using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application;
public static class ServicesExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(ServicesExtensions).Assembly;
        services.AddMediatR(config => config.RegisterServicesFromAssembly(assembly));
        services.AddValidatorsFromAssembly(assembly);
    }
}

