using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicAPP.DTO;

namespace BasicAPP.Interfaces
{
    public interface IVolantesApiProvider
    {
        public Task<List<VolantesDTO>> GetAsync();
        public Task<VolantesDTO?> PostAsync(VolantesDTO content);
    }
}
