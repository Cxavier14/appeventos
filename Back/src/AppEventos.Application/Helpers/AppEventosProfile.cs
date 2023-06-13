using AppEventos.Application.DTOs;
using AppEventos.Domain;
using AutoMapper;

namespace AppEventos.Application.Helpers
{
    public class AppEventosProfile : Profile
    {
        public AppEventosProfile()
        {
            CreateMap<Evento, EventoDTO>().ReverseMap();
            CreateMap<Lote, LoteDTO>().ReverseMap();
            CreateMap<Palestrante, PalestranteDTO>().ReverseMap();
            CreateMap<RedeSocial, RedeSocialDTO>().ReverseMap();
        }
    }
}
