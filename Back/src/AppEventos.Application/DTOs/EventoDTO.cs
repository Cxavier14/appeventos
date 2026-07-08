using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppEventos.Application.DTOs
{
    public class EventoDTO
    {
        public int Id { get; set; }
        public string Local { get; set; }

        [Required(ErrorMessage = "{0} é obrigatória.")]
        [Display(Name = "Data do evento")]
        public string DataEvento { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório."),
            StringLength(50, MinimumLength = 3, ErrorMessage = "{0} deve ter entre 3 e 50 caracteres.")]
        public string Tema { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório."),
            Display(Name = "Quantidade de pessoas"),
            Range(1, 120000, ErrorMessage = "{0} deve ter no mínimo 1 e no máximo 120.000.")]
        public int QtdPessoas { get; set; }

        [RegularExpression(@".*\.(gif|jpe?g|png|bmp)$", ErrorMessage = "Tipo não suportado.(gif, jpg, jpeg, png ou bmp).")]
        public string ImagemURL { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório.")]
        [Phone(ErrorMessage = "{0} digitado não é um telefone válido.")]
        public string Telefone { get; set; }

        [Display(Name = "e-mail")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        [EmailAddress(ErrorMessage = "{0} precisa ser um e-mail válido.")]
        public string Email { get; set; }
        public IEnumerable<LoteDTO> Lotes { get; set; }
        public IEnumerable<RedeSocialDTO> RedesSociais { get; set; }
        public IEnumerable<PalestranteDTO> Palestrantes { get; set; }
    }
}