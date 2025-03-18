using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WPF_CHEAT_os.Utils
{
    public static class HttpJsonClient<T>
    {
        private static readonly HttpClient httpClient = new HttpClient(); 

        public static async Task<T?> Get(string path)
        {
            HttpResponseMessage response = await httpClient.GetAsync($"{Constants.BASE_URL}{path}");
            string data = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(data);
        }

        public static async Task<T?> Post(string path, object data)
        {
            HttpContent content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await httpClient.PostAsync($"{Constants.BASE_URL}{path}", content);
            string responseData = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(responseData);
        }

        public static async Task<bool> Put(string path, object data)
        {
            HttpContent content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await httpClient.PutAsync($"{Constants.BASE_URL}{path}", content);
            return response.IsSuccessStatusCode;
        }

        public static async Task<bool> Delete(string path)
        {
            HttpResponseMessage response = await httpClient.DeleteAsync($"{Constants.BASE_URL}{path}");
            return response.IsSuccessStatusCode;
        }
    }
}
