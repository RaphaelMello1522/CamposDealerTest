using CamposDealerTest.Models;
using CamposDealerTest.Services.Interface;
using Newtonsoft.Json;
using System.Text.Json;

namespace CamposDealerTest.Services
{
    public class ClienteService : IClienteService
    {
        private readonly HttpClient _httpClient;
        private JsonSerializerOptions _options;

        public ClienteService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<List<Cliente>> GetClientesAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("cliente");

                var content = await response.Content.ReadAsStringAsync();
                var jsonResult = JsonConvert.DeserializeObject(content).ToString();
                var result = JsonConvert.DeserializeObject<List<Cliente>>(jsonResult);
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
