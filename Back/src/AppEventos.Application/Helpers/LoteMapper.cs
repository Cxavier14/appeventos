using AppEventos.Application.DTOs;
using AppEventos.Domain;
using Riok.Mapperly.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppEventos.Application.Helpers
{
    [Mapper]
    public static partial class LoteMapper
    {
        // ---------- Lote ----------
        //[MapperIgnoreTarget(nameof(LoteDTO.Evento))]
        //[MapperIgnoreSource(nameof(Lote.Evento))]
        [MapProperty(nameof(Lote.DataInicio), nameof(LoteDTO.DataInicio), Use = nameof(ParseDateTimeKind))]
        [MapProperty(nameof(Lote.DataFim), nameof(LoteDTO.DataFim), Use = nameof(ParseDateTimeKind))]
        public static partial LoteDTO ToDto(Lote lote);

        //[MapperIgnoreTarget(nameof(Lote.Evento))]
        //[MapperIgnoreSource(nameof(LoteDTO.Evento))]
        [MapProperty(nameof(LoteDTO.DataInicio), nameof(Lote.DataInicio), Use = nameof(ParseDateTimeKind))]
        [MapProperty(nameof(LoteDTO.DataFim), nameof(Lote.DataFim), Use = nameof(ParseDateTimeKind))]
        public static partial Lote ToEntity(LoteDTO dto);

        [MapProperty(nameof(LoteDTO.DataInicio), nameof(Lote.DataInicio), Use = nameof(ParseDateTimeKind))]
        [MapProperty(nameof(LoteDTO.DataFim), nameof(Lote.DataFim), Use = nameof(ParseDateTimeKind))]
        public static partial void UpdateEntity(LoteDTO dto, Lote entity);

        [MapProperty(nameof(LoteDTO.DataInicio), nameof(Lote.DataInicio), Use = nameof(ParseDateTimeKind))]
        [MapProperty(nameof(LoteDTO.DataFim), nameof(Lote.DataFim), Use = nameof(ParseDateTimeKind))]
        public static partial List<LoteDTO> ToDTOList(IEnumerable<Lote> lotes);

        private static DateTime? ParseDateTimeKind(DateTime? date)
        {
            if (date.GetValueOrDefault().Kind == DateTimeKind.Utc)
                return date;

            return DateTime.SpecifyKind(date.GetValueOrDefault(), DateTimeKind.Utc);
        }
    }
}
