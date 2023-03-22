using System.Collections.Generic;

namespace AppEventos.Domain
{
    public class PalestranteEvento 
    {
        public int PalestranteId { get; set; }
        public Palestrante Palestrante { get; set; }
        public int EventoId { get; set; }
        public Evento Evento { get; set; }
    }
}
