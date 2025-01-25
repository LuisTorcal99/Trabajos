using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeRogue.Interfaces
{
    public interface IHttpJsonClientProvider<T> where T : class
    {
        Task<T> Get(string url);
        Task<List<T>?> GetList(string url);
        Task<T?> Post(string url, T data);
        Task<T?> Put(string url, T data);
        Task<bool> Delete(string url);
    }
}
