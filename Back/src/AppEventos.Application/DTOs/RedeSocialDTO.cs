using System.ComponentModel.DataAnnotations;

namespace AppEventos.Application.DTOs
{
    public class RedeSocialDTO
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "{0} é obrigatório."),
            StringLength(100, MinimumLength = 1, ErrorMessage = "{0} deve ter entre 1 e 100 caracteres.")]
        public string Nome { get; set; }
        public string URL { get; set; }
        public int? EventoId { get; set; }
        public EventoDTO Evento { get; set; }
        public int? PalestranteId { get; set; }
        public PalestranteDTO Palestrante { get; set; }
    }
}
