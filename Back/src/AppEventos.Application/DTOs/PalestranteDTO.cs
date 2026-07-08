using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppEventos.Application.DTOs
{
    public class PalestranteDTO
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "{0} é obrigatório."),
            StringLength(50, MinimumLength = 3, ErrorMessage = "{0} deve ter entre 3 e 50 caracteres.")]
        public string Nome { get; set; }
        public string MiniCurriculo { get; set; }
        public string ImagemURL { get; set; }
        
        [Phone(ErrorMessage = "{0} digitado não é um telefone válido.")]
        public string Telefone { get; set; }
        
        [EmailAddress(ErrorMessage = "{0} precisa ser um e-mail válido.")]
        [Display(Name = "e-mail")]
        public string Email { get; set; }
        public IEnumerable<RedeSocialDTO> RedesSociais { get; set; }
        public IEnumerable<PalestranteDTO> Palestrantes { get; set; }
    }
}
