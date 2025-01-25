using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeRogue.Interfaces
{
    public interface IApiProvider<T> where T : class
    {
        Task<T?> GetDataAsync<T>(string url) where T : class, new();
    }
}
