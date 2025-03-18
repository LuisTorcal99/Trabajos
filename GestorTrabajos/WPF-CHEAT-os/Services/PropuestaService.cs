using WPF_CHEAT_os.DTO;
using WPF_CHEAT_os.Interfaces;
using WPF_CHEAT_os.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WPF_CHEAT_os.Services
{
    public class PropuestaService : IPropuestaProvider
    {
        private readonly IHttpsJsonClientProvider<PropuestaDTO> _httpsJsonClientProvider;

        public PropuestaService(IHttpsJsonClientProvider<PropuestaDTO> httpsJsonClientProvider)
        {
            _httpsJsonClientProvider = httpsJsonClientProvider;
        }

        public async Task<IEnumerable<PropuestaDTO>> GetAsync()
        {
            return await _httpsJsonClientProvider.GetAsync(Constants.PROPUESTA_PATH);
        }

        public async Task<PropuestaDTO> GetByIdAsync(string id)
        {
            return await _httpsJsonClientProvider.GetByIdAsync(Constants.PROPUESTA_PATH, id);
        }

        public async Task AddAsync(PropuestaDTO propuesta)
        {
            try
            {
                if (propuesta == null) return;
                await _httpsJsonClientProvider.PostAsync(Constants.PROPUESTA_PATH, propuesta);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task UpdateAsync(PropuestaDTO propuesta)
        {
            try
            {
                if (propuesta == null) return;
                await _httpsJsonClientProvider.PutAsync($"{Constants.PROPUESTA_PATH}/{propuesta.Id}", propuesta);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task DeleteAsync(string id)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(id)) return;
                await _httpsJsonClientProvider.DeleteAsync(Constants.PROPUESTA_PATH, id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}