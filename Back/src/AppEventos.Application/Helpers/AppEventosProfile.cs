using AppEventos.Application.DTOs;
using AppEventos.Domain;
using AutoMapper;

namespace AppEventos.Application.Helpers
{
    public class AppEventosProfile : Profile
    {
        public AppEventosProfile()
        {
            CreateMap<Evento, EventoDTO>();
        }
    }
}
