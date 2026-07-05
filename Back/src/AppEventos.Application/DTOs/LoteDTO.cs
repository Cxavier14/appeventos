using System.ComponentModel.DataAnnotations;

namespace AppEventos.Application.DTOs
{
    public class LoteDTO
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "{0} é obrigatório."),
            StringLength(50, MinimumLength = 3, ErrorMessage = "{0} deve ter entre 3 e 50 caracteres.")]
        public string Nome { get; set; }
        
        [Required(ErrorMessage = "{0} é obrigatório."),
            Display(Name = "Preço")]
        public decimal Preco { get; set; }
        
        [Required(ErrorMessage = "{0} é obrigatória."),
            Display(Name = "Data de início")]
        public string DataInicio { get; set; }
        
        [Required(ErrorMessage = "{0} é obrigatória."),
            Display(Name = "Data do fim")]
        public string DataFim { get; set; }
        
        [Required(ErrorMessage = "{0} é obrigatório."),
            Display(Name = "Quantidade"),
            Range(1, 120000)]
        public int Quantidade { get; set; }
        public int EventoId { get; set; }
        public EventoDTO Evento { get; set; }
    }
}
