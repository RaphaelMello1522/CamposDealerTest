using CamposDealerTest.Models;
using CamposDealerTest.Services.Interface;
using Newtonsoft.Json;
using System.Text.Json;

namespace CamposDealerTest.Services
{
    public class VendaService : IVendaService
    {
        private readonly HttpClient _httpClient;
        private JsonSerializerOptions _options;

        public VendaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<List<Venda>> GetVendasAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("venda");

                var content = await response.Content.ReadAsStringAsync();
                var jsonResult = JsonConvert.DeserializeObject(content).ToString();
                var result = JsonConvert.DeserializeObject<List<Venda>>(jsonResult);
                return result;
            }
            catch (Exception ex) when (LogException(ex))
            {
                Console.WriteLine("A excessão foi manuseada com sucesso!");
                return null;
            }

        }
        private static bool LogException(Exception ex)
        {
            Console.WriteLine($"Excessão captada {ex.GetType()}");
            return false;
        }
    }
}
