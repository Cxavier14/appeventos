using AppEventos.Persistence.Context;
using AppEventos.Persistence.IRepositories;
using AppEventos.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AppEventos.Persistence
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppEventosContext>(context => 
                context.UseNpgsql(configuration.GetConnectionString("Default"))
            );

            services.AddScoped<IBasePersistence, BasePersistence>();
            services.AddScoped<IEventoPersistence, EventoPersistence>();
            services.AddScoped<ILotePersistence, LotePersistence>();

            return services;
        }
    }
}
