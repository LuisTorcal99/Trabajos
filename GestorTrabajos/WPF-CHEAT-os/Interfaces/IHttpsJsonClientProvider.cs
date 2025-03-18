using System.Net.Http;
using WPF_CHEAT_os.DTO;

namespace WPF_CHEAT_os.Interfaces
{
    public interface IHttpsJsonClientProvider<T> where T : class
    {
        Task<IEnumerable<T>> GetAsync(string api_url);
        Task<T?> GetByIdAsync(string path, string id);
        Task<T?> PostAsync(string path, T data);
        Task<T?> PutAsync(string path, T data);
        Task Authenticate(string path, HttpClient httpClient, HttpResponseMessage request);
        Task<T?> LoginPostAsync(string path, LoginDTO data);
        Task<T?> RegisterPostAsync(string path, RegistroDTO data);
        Task<bool> DeleteAsync(string path, string id);
    }
}
