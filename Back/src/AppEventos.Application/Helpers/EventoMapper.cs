using AppEventos.Application.DTOs;
using AppEventos.Domain;
using Riok.Mapperly.Abstractions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace AppEventos.Application.Helpers
{
    [Mapper]
    public static partial class EventoMapper
    {
        // ---------- Evento ----------
        //[MapProperty(nameof(Evento.DataEvento), nameof(EventoDTO.DataEvento), Use = nameof(DateTimeToString))]
        public static partial EventoDTO ToDto(Evento evento);

        [MapperIgnoreTarget(nameof(Evento.PalestrantesEventos))]
        //[MapperIgnoreSource(nameof(EventoDTO.Palestrantes))]
        //[MapProperty(nameof(EventoDTO.DataEvento), nameof(Evento.DataEvento), Use = nameof(StringToDateTime))]
        public static partial Evento ToEntity(EventoDTO dto);

        [MapperIgnoreTarget(nameof(Evento.PalestrantesEventos))]
        //[MapperIgnoreSource(nameof(EventoDTO.Palestrantes))]
        //[MapProperty(nameof(EventoDTO.DataEvento), nameof(Evento.DataEvento), Use = nameof(StringToDateTime))]
        public static partial void UpdateEntity(EventoDTO dto, Evento entity);

        // ---------- RedeSocial ----------
        [MapperIgnoreTarget(nameof(RedeSocialDTO.Evento))]
        [MapperIgnoreSource(nameof(RedeSocial.Evento))]
        [MapperIgnoreTarget(nameof(RedeSocialDTO.Palestrante))]
        [MapperIgnoreSource(nameof(RedeSocial.Palestrante))]
        public static partial RedeSocialDTO ToDto(RedeSocial redeSocial);

        [MapperIgnoreTarget(nameof(RedeSocial.Evento))]
        [MapperIgnoreSource(nameof(RedeSocialDTO.Evento))]
        [MapperIgnoreTarget(nameof(RedeSocial.Palestrante))]
        [MapperIgnoreSource(nameof(RedeSocialDTO.Palestrante))]
        public static partial RedeSocial ToEntity(RedeSocialDTO dto);

        // ---------- Palestrante ----------        
        [MapperIgnoreSource(nameof(Palestrante.PalestrantesEventos))]
        [MapperIgnoreTarget(nameof(PalestranteDTO.Palestrantes))]
        public static partial PalestranteDTO ToDto(Palestrante palestrante);

        [MapperIgnoreSource(nameof(PalestranteDTO.RedesSociais))]
        [MapperIgnoreTarget(nameof(Palestrante.RedesSociais))]      // opcional, ver nota abaixo
        [MapperIgnoreSource(nameof(PalestranteDTO.Palestrantes))]
        [MapperIgnoreTarget(nameof(Palestrante.PalestrantesEventos))]
        public static partial Palestrante ToEntity(PalestranteDTO dto);

        // achata a tabela de junção PalestranteEvento -> PalestranteDTO
        private static PalestranteDTO ToDto(PalestranteEvento pe) => ToDto(pe.Palestrante);

        // ---------- mapeamento de listas ----------
        public static partial List<EventoDTO> ToDTOList(IEnumerable<Evento> eventos);

        // ---------- Conversões manuais ----------
        private static string DateTimeToString(DateTime? date) =>
            date?.ToString("dd/MM/yyyy") ?? string.Empty;

        private static DateTime? StringToDateTime(string date){
            if (!DateTime.TryParseExact(
                date,
                "dd/MM/yyyy",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out var parsed)) return null;

            return DateTime.SpecifyKind(parsed, DateTimeKind.Utc);
        }
    }
}
