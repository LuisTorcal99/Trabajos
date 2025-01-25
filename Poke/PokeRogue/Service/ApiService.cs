using System;
using System.Collections.Generic;
using System.Linq;
using PokeRogue.Interfaces;
using PokeRogue.Service;


namespace PokeRogue.Services
{
    public class ApiService<T> : IApiProvider<T> where T : class
    {
        private readonly HttpJsonClientService<object> _httpClient;

        public ApiService(HttpJsonClientService<object> httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<T?> GetDataAsync<T>(string url) where T : class, new()
        {
            T? requestData = null;

            do
            {
                requestData = await _httpClient.Get(url) as T ?? new T();
            } while (requestData == null);

            return requestData;
            // USO en view model: PokemonStats = await _apiService.GetDataAsync<PokemonStatsModel>(url) ?? new PokemonStatsModel();
        }
    }
}

