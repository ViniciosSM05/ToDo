using DDDApi.Application.Applications;
using DDDApi.Domain.Core.Interfaces.Application;
using DDDApi.Domain.Core.Interfaces.Auth;
using DDDApi.Domain.Core.Interfaces.Notification;
using DDDApi.Domain.Core.Interfaces.Repository;
using DDDApi.Domain.Core.Interfaces.Service;
using DDDApi.Domain.Notifications;
using DDDApi.Domain.Mappers;
using DDDApi.Domain.Core.DTO.User;
using DDDApi.Domain.Validators;
using DDDApi.Infra.Auth;
using DDDApi.Infra.Data.Context;
using DDDApi.Infra.Data.Repository;
using DDDApi.Service.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

namespace DDDApi.Infra.IoC
{
    public static class IoC
    {
        public static void AddDependencies(this IServiceCollection services, IConfiguration configuration) =>
            services
                .AddSwagger()
                .AddHttpContextAccessor()
                .AddData(configuration)
                .AddAuth(configuration)
                .AddNotifications()
                .AddValidators()
                .AddMappers()
                .AddServices()
                .AddApplications();

        public static void StartDatabase(this IServiceScope serviceScope)
        {
            using var context = serviceScope.ServiceProvider.GetService<IEntityContext>();
            context.Database.Migrate();
        }

        private static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"Autenticação por token JWT. Entre com o valor no formato: Bearer SEU_TOKEN",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
            });

            return services;
        }

        private static IServiceCollection AddData(this IServiceCollection services, IConfiguration configuration)
        {
            var connection = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<IEntityContext, EntityContext>(options =>
                options.UseMySql(connection, ServerVersion.AutoDetect(connection)), ServiceLifetime.Scoped);

            services.AddScoped<IRepositoryUser, RepositoryUser>();
            services.AddScoped<IRepositoryTodo, RepositoryTodo>();
            return services;
        }

        private static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
        {
            var key = Encoding.ASCII.GetBytes(configuration.GetValue<string>("Auth:Key"));
            services
                .AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new()
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            services.AddScoped<IAuthToken, AuthToken>();
            services.AddScoped<IAuthInfo, AuthInfo>();
            return services;
        }

        private static IServiceCollection AddValidators(this IServiceCollection services)
            => services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

        private static IServiceCollection AddNotifications(this IServiceCollection services)
            => services.AddScoped<INotification, Notification>();

        private static IServiceCollection AddMappers(this IServiceCollection services)
            => services
                .AddAutoMapper(typeof(MapperUser))
                .AddAutoMapper(typeof(MapperTodo));

        private static IServiceCollection AddServices(this IServiceCollection services)
            => services
                .AddScoped<IServiceUser, ServiceUser>()
                .AddScoped<IServiceTodo, ServiceTodo>();

        private static IServiceCollection AddApplications(this IServiceCollection services)
            => services
                .AddScoped<IApplicationUser, ApplicationUser>()
                .AddScoped<IApplicationTodo, ApplicationTodo>();

    }
}
