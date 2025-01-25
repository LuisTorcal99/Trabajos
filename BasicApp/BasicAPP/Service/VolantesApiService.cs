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
        public async Task<List<VolantesDTO>> GetAsync()
        {
            return await HttpJsonClient<List<VolantesDTO>>.Get(Constantes.VOLANTES_PATH);
        }
        public async Task<VolantesDTO?> PostAsync(VolantesDTO content)
        {
            return await HttpJsonClient<VolantesDTO>.Post(Constantes.VOLANTES_PATH, content);
        }
    }
}
