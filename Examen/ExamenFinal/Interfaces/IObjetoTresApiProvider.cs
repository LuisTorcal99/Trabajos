using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExamenFinal.DTO;

namespace ExamenFinal.Interfaces
{
    public interface IObjetoTresApiProvider
    {
        Task<IEnumerable<ObjetoTresDTO>> GetObjetoTres();
        Task<ObjetoTresDTO> GetOneObjetoTres(string id);
        Task PostObjetoTres(ObjetoTresDTO ObjetoTres);
        Task PatchObjetoTres(ObjetoTresDTO ObjetoTres);
        Task<bool> DeleteObjetoTres(string id);
    }
}
