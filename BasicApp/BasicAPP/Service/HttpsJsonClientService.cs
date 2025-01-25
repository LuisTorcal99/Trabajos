using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BasicAPP.DTO;
using BasicAPP.Interfaces;
using BasicAPP.Utils;

namespace BasicAPP.Service
{
    public class HttpsJsonClientService<T> : IHttpsJsonClientProvider<T> where T : class
    {
        private readonly HttpClient _httpClient;
        private string _token;

        public HttpsJsonClientService(string baseUrl, string token = "")
        {
            _httpClient = new HttpClient { BaseAddress = new Uri(baseUrl) };
            _token = token;

            LoginDTO loginDTO = new LoginDTO
            {
                Email = "luis@gmail.com",
                Password = "gasdgSDG99A."
            };

            if (!string.IsNullOrEmpty(_token))
            {
                _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_token}");
            }
        }

        public async Task<T?> Get(string path)
        {
            try
            {
                HttpResponseMessage request = await _httpClient.GetAsync(path);

                if (request.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    await RefreshTokenAsync();
                    request = await _httpClient.GetAsync(path);
                }

                if (!request.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Error en la solicitud GET: {request.StatusCode}");
                    return default;
                }

                string dataRequest = await request.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(dataRequest);
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error al deserializar los datos: {ex.Message}");
                return default;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en Get: {ex.Message}");
                return default;
            }
        }

        private async Task RefreshTokenAsync()
        {
            try
            {
                LoginDTO loginDTO = new LoginDTO
                {
                    Email = "luis@gmail.com",
                    Password = "gasdgSDG99A."
                };

                HttpContent httpContent = CreateJsonContent(loginDTO);
                HttpResponseMessage response = await _httpClient.PostAsync(Constantes.LOGIN_PATH + "/login", httpContent);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Error al obtener un nuevo token: {response.StatusCode}");
                    return;
                }

                string dataTokenRequest = await response.Content.ReadAsStringAsync();
                UserDTO tokenUser = JsonSerializer.Deserialize<UserDTO>(dataTokenRequest);

                _token = tokenUser?.Result?.Token ?? string.Empty;

                if (!string.IsNullOrEmpty(_token))
                {
                    _httpClient.DefaultRequestHeaders.Remove("Authorization");
                    _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_token}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al refrescar el token: {ex.Message}");
                throw;
            }
        }

        private HttpContent CreateJsonContent(object data)
        {
            return new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
        }

        public async Task<T> Post(string url, T content)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(url, content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }


        public async Task<T> Put(string url, T content)
        {
            HttpResponseMessage response = await _httpClient.PutAsJsonAsync(url, content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }

        public async Task<bool> Delete(string url)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync(url);
            response.EnsureSuccessStatusCode();
            return response.IsSuccessStatusCode;
        }

        public async Task<T> Patch(string url, object patchData)
        {
            HttpRequestMessage request = new HttpRequestMessage(new HttpMethod("PATCH"), url)
            {
                Content = JsonContent.Create(patchData)
            };

            HttpResponseMessage response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }
    }
}