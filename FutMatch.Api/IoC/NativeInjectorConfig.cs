using FutMatch.Application.Services;
using FutMatch.Application.Services.Interfaces;
using FutMatch.Domain.Repositories;
using FutMatch.Infra.Data.Repositories;

namespace FutMatch.Api.IoC
{
    public static class NativeInjectorConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {

            //Services
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IPlayerService, PlayerService>();


            //Repositories
            services.AddScoped<IPlayerRepository, PlayerRepository>();
            services.AddScoped<ITeamRepository, TeamRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IFieldPositionRepository, FieldPositionRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
