using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExamenFinal.DTO;
using ExamenFinal.Interfaces;
using ExamenFinal.Utils;

namespace ExamenFinal.Service
{
    public class ObjetoTresApiService : IObjetoTresApiProvider
    {
        private readonly IHttpsJsonClientProvider<ObjetoTresDTO> _httpsJsonClientProvider;

        public ObjetoTresApiService(IHttpsJsonClientProvider<ObjetoTresDTO> httpsJsonClientProvider)
        {
            _httpsJsonClientProvider = httpsJsonClientProvider;
        }

        public async Task<IEnumerable<ObjetoTresDTO>> GetObjetoTres()
        {
            return await _httpsJsonClientProvider.GetAsync(Constantes.OBJETO_TRES_PATH);
        }

        public async Task<ObjetoTresDTO> GetOneObjetoTres(string id)
        {
            return await _httpsJsonClientProvider.GetByIdAsync(Constantes.OBJETO_TRES_PATH, id);
        }

        public async Task PostObjetoTres(ObjetoTresDTO ObjetoTres)
        {
            try
            {
                if (ObjetoTres == null) return;
                await _httpsJsonClientProvider.PostAsync(Constantes.OBJETO_TRES_PATH, ObjetoTres);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear el ObjetoTres: {ex.Message}");
            }
        }

        public async Task PatchObjetoTres(ObjetoTresDTO ObjetoTres)
        {
            try
            {
                if (ObjetoTres == null) return;
                await _httpsJsonClientProvider.PatchAsync($"{Constantes.OBJETO_TRES_PATH}/{ObjetoTres.Id}", ObjetoTres);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar el ObjetoTres: {ex.Message}");
            }
        }

        public async Task<bool> DeleteObjetoTres(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id)) return false;
                return await _httpsJsonClientProvider.DeleteAsync(Constantes.OBJETO_TRES_PATH, id);
            }
            
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar el ObjetoTres: {ex.Message}");
                return false;
            }
        }
    }
}
