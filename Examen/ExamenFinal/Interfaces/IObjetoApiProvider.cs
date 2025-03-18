using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExamenFinal.DTO;

namespace ExamenFinal.Interfaces
{
    public interface IObjetoApiProvider
    {
        Task<IEnumerable<ObjetoDTO>> GetObjeto();
        Task<ObjetoDTO> GetOneObjeto(string id);
        Task PostObjeto(ObjetoDTO Objeto);
        Task PatchObjeto(ObjetoDTO Objeto);
        Task<bool> DeleteObjeto(string id);
    }
}
