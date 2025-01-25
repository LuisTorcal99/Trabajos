using System.Threading.Tasks;

namespace BasicAPP.Interfaces
{
    public interface IHttpsJsonClientProvider<T> where T : class
    {
        Task<T> Get(string url);
        Task<T> Post(string url, T content);
        Task<T> Put(string url, T content);
        Task<bool> Delete(string url);
        Task<T> Patch(string url, object patchData);
    }
}
