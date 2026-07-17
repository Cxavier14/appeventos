using AppEventos.Application.DTOs;
using AppEventos.Application.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AppEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LotesController(IEventoService eventoService, ILoteService loteService) : ControllerBase
    {
        private readonly IEventoService _eventoService = eventoService;
        private readonly ILoteService _loteService = loteService;

        [HttpGet("{eventoId}")]
        public async Task<IActionResult> Get(int eventoId)
        {
            try
            {
                var eventos = await _loteService.GetAllEventosAsync(true);
                if (eventos == null) return NoContent();

                return Ok(eventos);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar recuperar eventos. Erro: {e.Message}");
            }
        }

        [HttpPut("{eventoId}")]
        public async Task<IActionResult> Put(int eventoId, EventoDTO[] models)
        {
            try
            {
                var evento = await _loteService.UpdateEvento(id, models);
                if (evento == null) return NoContent();

                return Ok(evento);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar atualizar evento. Erro: {e.Message}");
            }
        }

        [HttpDelete("{eventoId}/{loteId}")]
        public async Task<IActionResult> Delete(int eventoId, int loteId)
        {
            try
            {
                var result = await _loteService.GetEventoByIdAsync(id);
                if (result == null) return NoContent();

                return await _loteService.DeleteEvento(id)
                    ? Ok(new { message = "Deletado" })
                    : throw new Exception("Ocorreu um erro inesperado!");
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar deletar evento. Erro: {e.Message}");
            }
        }
    }
}