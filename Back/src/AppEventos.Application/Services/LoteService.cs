using AppEventos.Application.DTOs;
using AppEventos.Application.Helpers;
using AppEventos.Application.IServices;
using AppEventos.Domain;
using AppEventos.Persistence.IRepositories;
using AppEventos.Persistence.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppEventos.Application.Services
{
    public class LoteService(IBasePersistence basePersistence, ILotePersistence lotePersistence, ILogger<LoteService> logger) : ILoteService
    {
        private readonly ILogger<LoteService> _logger = logger;
        private readonly IBasePersistence _basePersistence = basePersistence;
        private readonly ILotePersistence _lotePersistence = lotePersistence;

        public async Task<bool> DeleteLote(int eventoId, int id)
        {
            try
            {
                var lote = await _lotePersistence.GetLoteByIdsAsync(eventoId, id);
                if (lote is null)
                    throw new Exception($"Erro ao tentar deletar o Lote de Id: {id} do EventoId: {eventoId}.");

                _basePersistence.Delete<Lote>(lote);
                return await _basePersistence.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao tentar deletar o Lote de Id: {id} do EventoId: {eventoId}", id, eventoId);
                throw;
            }
        }

        public async Task<List<LoteDTO>> GetLotesByEventoIdAsync(int eventoId)
        {
            try
            {
                var lotes = await _lotePersistence.GetLotesByEventoIdAsync(eventoId);
                if ((lotes is null) || (lotes.Count <= 0))
                    return null;

                return LoteMapper.ToDTOList(lotes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao tentar recuperar os Lotes para o EventoId: {eventoId}", eventoId);
                throw;
            }
        }

        public async Task<LoteDTO> GetLoteByIdsAsync(int eventoId, int id)
        {
            try
            {
                var lote = await _lotePersistence.GetLoteByIdsAsync(eventoId, id);
                if (lote is null)
                    return null;

                return LoteMapper.ToDto(lote);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao tentar recuperar Lote por Id: {id} para o EventoId: {eventoId}", id, eventoId);
                throw;
            }
        }

        public async Task<List<LoteDTO>> SaveLotes(int eventoId, List<LoteDTO> lotesList)
        {
            var lotes = await _lotePersistence.GetLotesByEventoIdAsync(eventoId);
            if ((lotesList is null) || (lotesList.Count <= 0))
                return null;

            foreach (var model in lotesList)
            {
                if (model.Id <= 0)
                    await SaveLote(eventoId, model);
                else
                    await UpdateLote(eventoId, lotes, model);
            }

            var result = await _lotePersistence.GetLotesByEventoIdAsync(eventoId);
            return LoteMapper.ToDTOList(result);
        }

        private async Task SaveLote(int eventoId, LoteDTO model)
        {
            try
            {
                var lote = LoteMapper.ToEntity(model);
                lote.EventoId = eventoId;

                _basePersistence.Save<Lote>(lote);

                await _basePersistence.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao tentar salvar o lote.");
                throw;
            }
        }

        private async Task UpdateLote(int eventoId, List<Lote> lotes, LoteDTO model)
        {
            try
            {
                var lote = lotes.FirstOrDefault(l => l.Id == model.Id);
                if (lote is null)
                    throw new Exception($"Lote de Id: {model.Id} não encontrado para o EventoId: {eventoId}.");

                model.EventoId = eventoId;
                LoteMapper.UpdateEntity(model, lote);

                _basePersistence.Update<Lote>(lote);
                await _basePersistence.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao tentar salvar o lote.");
                throw;
            }
        }
    }
}
