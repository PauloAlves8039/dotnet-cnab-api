using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace CNAB.Infra.IoC.Configurations;

public static class DependencyInjectionSwagger
{
    public static IServiceCollection AddInfrastructureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "CNAB - API",
                    Version = "v1",
                    Description = "API to manage and normalize CNAB transaction data.",
                    Contact = new OpenApiContact
                    {
                        Name = "Paulo Alves",
                        Email = "pj38alves@gmail.com",
                        Url = new Uri("https://github.com/PauloAlves8039"),
                    },
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] " +
                                  "and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });
        

        return services;
    }
}