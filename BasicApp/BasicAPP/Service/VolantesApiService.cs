using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicAPP.DTO;
using BasicAPP.Interfaces;
using BasicAPP.Utils;
using Microsoft.VisualBasic;

namespace BasicAPP.Service
{
    public class VolantesApiService : IVolantesApiProvider
    {
        public readonly IHttpsJsonClientProvider<VolantesDTO> _httpsJsonClientProvider;
        public VolantesApiService(IHttpsJsonClientProvider<VolantesDTO> httpsJsonClientProvider)
        {
            _httpsJsonClientProvider = httpsJsonClientProvider;
        }

        public async Task<IEnumerable<VolantesDTO>> GetVolantes()
        {
            IEnumerable<VolantesDTO> Volantes = await _httpsJsonClientProvider.GetAsync(Constantes.VOLANTES_PATH);

            return Volantes;
        }

        public async Task PostVolantes(VolantesDTO volante)
        {
            try
            {
                if (volante == null) return;
                var response = await _httpsJsonClientProvider.PostAsync(Constantes.VOLANTES_PATH, volante);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task PutVolantes(VolantesDTO volante)
        {
            try
            {
                if (volante == null) return;
                var response = await _httpsJsonClientProvider.PutAsync(Constantes.VOLANTES_PATH + "/" + volante.Id, volante);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
