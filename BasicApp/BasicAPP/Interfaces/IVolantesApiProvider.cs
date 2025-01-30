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
        Task<IEnumerable<VolantesDTO>> GetVolantes();

        Task PostVolantes(VolantesDTO volantes);

        Task PutVolantes(VolantesDTO volantes);
    }
}
