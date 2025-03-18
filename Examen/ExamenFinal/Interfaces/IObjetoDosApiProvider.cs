using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExamenFinal.DTO;

namespace ExamenFinal.Interfaces
{
    public interface IObjetoDosApiProvider
    {
        Task<IEnumerable<ObjetoDosDTO>> GetObjetoDos();
        Task<ObjetoDosDTO> GetOneObjetoDos(string id);
        Task PostObjetoDos(ObjetoDosDTO Objeto);
        Task PatchObjetoDos(ObjetoDosDTO Objeto);
        Task<bool> DeleteObjetoDos(string id);
    }
}
