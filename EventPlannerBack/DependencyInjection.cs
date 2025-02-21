using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Models.Configuration;
using Domain.Repositories;
using infrastructure.MailService.Implementations;
using infrastructure.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace EventPlannerBack
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<IUserAuthorizationService,UserAuthorizationService>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IAuthCodesRepository, AuthCodesRepository>();
            services.AddTransient<IJWTTokenGenerator, JwtTokenGenerator>();
            services.AddTransient<IMailService, MailService>();
            services.AddTransient<IEventRepository, EventRepository>();
            services.AddTransient<IEventService, EventService>();
            services.AddTransient<IUserService, UserService>();
            return services;
        }
    }
}
