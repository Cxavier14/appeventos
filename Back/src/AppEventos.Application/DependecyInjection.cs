using AppEventos.Application.IServices;
using AppEventos.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AppEventos.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IEventoService, EventoService>();
            services.AddScoped<ILoteService, LoteService>();

            return services;
        }
    }
}
