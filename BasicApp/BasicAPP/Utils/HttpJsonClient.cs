using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BasicAPP.DTO;


namespace BasicAPP.Utils
{
    public static class HttpJsonClient<T>
    {
        public static string Token = string.Empty;
        public static async Task<T?> Get(string path)
        {
            try
            {
                using HttpClient httpClient = new HttpClient();
                {
                    httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {Token}");
                    HttpResponseMessage request = await httpClient.GetAsync($"{Constantes.BASE_URL}{path}");
                    if (request.StatusCode==System.Net.HttpStatusCode.Unauthorized)
                    {
                        LoginDTO loginDTO = new LoginDTO
                        {
                            Email = "luis@gmail.com",
                            Password = "gasdgSDG99A."
                        };
                        HttpContent httpContent = new StringContent(JsonSerializer.Serialize(loginDTO),  Encoding.UTF8, "application/json");

                        HttpResponseMessage requestToken = await httpClient.PostAsync($"{Constantes.BASE_URL}{Constantes.USERS_PATH}/login", httpContent);

                        string dataTokenRequest = await requestToken.Content.ReadAsStringAsync();
                        UserDTO tokenUser = JsonSerializer.Deserialize<UserDTO>(dataTokenRequest);

                        Token=tokenUser?.Result?.Token ??string.Empty;
                        httpClient.DefaultRequestHeaders.Remove("Authorization");
                        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {Token}");
                        request = await httpClient.GetAsync($"{Constantes.BASE_URL}{path}");
                    }
                    string dataRequest = await request.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<T>(dataRequest);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return default;
        }
        public static async Task<T?> Post(string path, T content)
        {
            try
            {
                using HttpClient httpClient = new HttpClient();
                {
                    httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {Token}");
                    HttpContent httpContent = new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, "application/json");
                    HttpResponseMessage request = await httpClient.PostAsync($"{Constantes.BASE_URL}{path}", httpContent);

                    if (request.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        await RenewToken(httpClient);
                        request = await httpClient.PostAsync($"{Constantes.BASE_URL}{path}", httpContent);
                    }

                    string dataRequest = await request.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<T>(dataRequest);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return default;
        }

        public static async Task<T?> Put(string path, T content)
        {
            try
            {
                using HttpClient httpClient = new HttpClient();
                {
                    httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {Token}");
                    HttpContent httpContent = new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, "application/json");
                    HttpResponseMessage request = await httpClient.PutAsync($"{Constantes.BASE_URL}{path}", httpContent);

                    if (request.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        await RenewToken(httpClient);
                        request = await httpClient.PutAsync($"{Constantes.BASE_URL}{path}", httpContent);
                    }

                    string dataRequest = await request.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<T>(dataRequest);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return default;
        }

        public static async Task<bool> Delete(string path)
        {
            try
            {
                using HttpClient httpClient = new HttpClient();
                {
                    httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {Token}");
                    HttpResponseMessage request = await httpClient.DeleteAsync($"{Constantes.BASE_URL}{path}");

                    if (request.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        await RenewToken(httpClient);
                        request = await httpClient.DeleteAsync($"{Constantes.BASE_URL}{path}");
                    }

                    return request.IsSuccessStatusCode;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public static async Task<T?> Patch(string path, T content)
        {
            try
            {
                using HttpClient httpClient = new HttpClient();
                {
                    httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {Token}");
                    HttpContent httpContent = new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, "application/json");
                    HttpRequestMessage patchRequest = new HttpRequestMessage(new HttpMethod("PATCH"), $"{Constantes.BASE_URL}{path}")
                    {
                        Content = httpContent
                    };
                    HttpResponseMessage request = await httpClient.SendAsync(patchRequest);

                    if (request.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        await RenewToken(httpClient);
                        request = await httpClient.SendAsync(patchRequest);
                    }

                    string dataRequest = await request.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<T>(dataRequest);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return default;
        }

        private static async Task RenewToken(HttpClient httpClient)
        {
            LoginDTO loginDTO = new LoginDTO
            {
                Email = "luis@gmail.com",
                Password = "gasdgSDG99A."
            };
            HttpContent httpContent = new StringContent(JsonSerializer.Serialize(loginDTO), Encoding.UTF8, "application/json");
            HttpResponseMessage requestToken = await httpClient.PostAsync($"{Constantes.BASE_URL}{Constantes.USERS_PATH}/login", httpContent);

            string dataTokenRequest = await requestToken.Content.ReadAsStringAsync();
            UserDTO tokenUser = JsonSerializer.Deserialize<UserDTO>(dataTokenRequest);

            Token = tokenUser?.Result?.Token ?? string.Empty;
            httpClient.DefaultRequestHeaders.Remove("Authorization");
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {Token}");
        }
    }
}
