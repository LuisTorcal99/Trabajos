using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExamenFinal.DTO;
using ExamenFinal.Interfaces;
using ExamenFinal.Utils;

namespace ExamenFinal.Service
{
    public class ObjetoApiService : IObjetoApiProvider
    {
        private readonly IHttpsJsonClientProvider<ObjetoDTO> _httpsJsonClientProvider;

        public ObjetoApiService(IHttpsJsonClientProvider<ObjetoDTO> httpsJsonClientProvider)
        {
            _httpsJsonClientProvider = httpsJsonClientProvider;
        }

        public async Task<IEnumerable<ObjetoDTO>> GetObjeto()
        {
            return await _httpsJsonClientProvider.GetAsync(Constantes.OBJETO_PATH);
        }

        public async Task<ObjetoDTO> GetOneObjeto(string id)
        {
            return await _httpsJsonClientProvider.GetByIdAsync(Constantes.OBJETO_PATH, id);
        }

        public async Task PostObjeto(ObjetoDTO objeto)
        {
            try
            {
                if (objeto == null) return;
                await _httpsJsonClientProvider.PostAsync(Constantes.OBJETO_PATH, objeto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear el objeto: {ex.Message}");
            }
        }

        public async Task PatchObjeto(ObjetoDTO objeto)
        {
            try
            {
                if (objeto == null) return;
                await _httpsJsonClientProvider.PatchAsync($"{Constantes.OBJETO_PATH}/{objeto.Id}", objeto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar el objeto: {ex.Message}");
            }
        }

        public async Task<bool> DeleteObjeto(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id)) return false;
                return await _httpsJsonClientProvider.DeleteAsync(Constantes.OBJETO_PATH, id);
            }
            
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar el objeto: {ex.Message}");
                return false;
            }
        }
    }
}
