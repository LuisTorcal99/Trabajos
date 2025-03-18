using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExamenFinal.DTO;
using ExamenFinal.Interfaces;
using ExamenFinal.Utils;

namespace ExamenFinal.Service
{
    public class ObjetoDosApiService : IObjetoDosApiProvider
    {
        private readonly IHttpsJsonClientProvider<ObjetoDosDTO> _httpsJsonClientProvider;

        public ObjetoDosApiService(IHttpsJsonClientProvider<ObjetoDosDTO> httpsJsonClientProvider)
        {
            _httpsJsonClientProvider = httpsJsonClientProvider;
        }

        public async Task<IEnumerable<ObjetoDosDTO>> GetObjetoDos()
        {
            return await _httpsJsonClientProvider.GetAsync(Constantes.OBJETO_DOS_PATH);
        }

        public async Task<ObjetoDosDTO> GetOneObjetoDos(string id)
        {
            return await _httpsJsonClientProvider.GetByIdAsync(Constantes.OBJETO_DOS_PATH, id);
        }

        public async Task PostObjetoDos(ObjetoDosDTO objeto)
        {
            try
            {
                if (objeto == null) return;
                await _httpsJsonClientProvider.PostAsync(Constantes.OBJETO_DOS_PATH, objeto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear el objeto: {ex.Message}");
            }
        }

        public async Task PatchObjetoDos(ObjetoDosDTO objeto)
        {
            try
            {
                if (objeto == null) return;
                await _httpsJsonClientProvider.PatchAsync($"{Constantes.OBJETO_DOS_PATH}/{objeto.Id}", objeto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar el objeto: {ex.Message}");
            }
        }

        public async Task<bool> DeleteObjetoDos(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id)) return false;
                return await _httpsJsonClientProvider.DeleteAsync(Constantes.OBJETO_DOS_PATH, id);
            }
            
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar el objeto: {ex.Message}");
                return false;
            }
        }
    }
}
