using DevFreela.Core.Repositories;
using DevFreela.Core.Services;
using DevFreela.Infrastructure.Auth;
using DevFreela.Infrastructure.CloudServices.Implementations;
using DevFreela.Infrastructure.CloudServices.Interfaces;
using DevFreela.Infrastructure.MessageBus;
using DevFreela.Infrastructure.Persistence.Repositories;

namespace DevFreela.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            // * SCOPED: OBJETO DIFERENTE PARA CADA REQUISIÇÃO (TEMPO DE VIDA PARA REQUISIÇÕES)
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISkillRepository, SkillRepository>();

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IPaymentsService, PaymentsService>();
            services.AddScoped<IMessageBusService, MessageBusService>();

            return services;
        }
    }
}