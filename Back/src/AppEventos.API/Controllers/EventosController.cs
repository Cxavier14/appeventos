using System;
using System.Collections.Generic;
using System.Linq;
using AppEventos.Persistence.Context;
using AppEventos.Domain;
using Microsoft.AspNetCore.Mvc;
using AppEventos.Application.IServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AppEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : ControllerBase
    {
        private readonly IEventoService _eventoService;

        public EventosController(IEventoService eventoService)
        {
            _eventoService = eventoService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var eventos = await _eventoService.GetAllEventosAsync(true);
                if(eventos == null) return NotFound("Nenhum evento encontrado");

                return Ok(eventos);
            }
            catch (Exception e)
            {                
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                $"Erro ao tentar recuperar eventos. Erro: {e.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var evento = await _eventoService.GetEventoByIdAsync(id, true);
                if(evento == null) return NotFound("Nenhum evento encontrado");

                return Ok(evento);
            }
            catch (Exception e)
            {                
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                $"Erro ao tentar recuperar evento. Erro: {e.Message}");
            }
        }

        [HttpGet("{tema}/tema")]
        public async Task<IActionResult> GetByTema(string tema)
        {
            try
            {
                var evento = await _eventoService.GetAllEventosByTemaAsync(tema, true);
                if(evento == null) return NotFound("Nenhum evento com este tema encontrado");

                return Ok(evento);
            }
            catch (Exception e)
            {                
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                $"Erro ao tentar recuperar evento. Erro: {e.Message}");
            }
        }

        [HttpPost]
        public string Post()
        {
            return "Exemplo de post";
        }

        [HttpPut("{id}")]
        public string Put(int id)
        {
            return $"Exemplo de put com id = {id}";
        }

        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            return $"Exemplo de delete com id = {id}";
        }
    }
}