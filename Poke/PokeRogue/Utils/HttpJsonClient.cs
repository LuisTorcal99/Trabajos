using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PokeRogue.Utils
{
    //TODO service
    public static class HttpJsonClient<T>
    {
        public static async Task<T?> Get(string url)
        {
            using HttpClient httpClient = new HttpClient();
            try
            {
                HttpResponseMessage datos = await httpClient.GetAsync(url);
                string dataget = await datos.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(dataget);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return default;
        }

        public static async Task<List<T>?> GetList(string url)
        {
            using HttpClient httpClient = new HttpClient();
            try
            {
                HttpResponseMessage datos = await httpClient.GetAsync(url);
                string dataget = await datos.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<T>>(dataget);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public static async Task<T?> Post(string url, T data)
        {
            using HttpClient httpClient = new HttpClient();

            string jsonData = JsonSerializer.Serialize(data);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            return default;
        }

        public static async Task<T?> Put(string url, T data)
        {
            using HttpClient httpClient = new HttpClient();

            try
            {
                string jsonData = JsonSerializer.Serialize(data);
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.PutAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<T>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"PUT error: {ex.Message}");
            }

            return default;
        }

        public static async Task<bool> Delete(string url)
        {
            using HttpClient httpClient = new HttpClient();

            try
            {
                HttpResponseMessage response = await httpClient.DeleteAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DELETE error: {ex.Message}");
            }

            return false;
        }
    }
}
