using CamposDealerTest.Data;
using CamposDealerTest.Models;
using CamposDealerTest.Services.Interface;
using Newtonsoft.Json;
using System.Text.Json;

namespace CamposDealerTest.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly HttpClient _httpClient;
        private JsonSerializerOptions _options;
        private CamposDealerTestContext _context;

        public ProdutoService(HttpClient httpClient, CamposDealerTestContext context)
        {
            _httpClient = httpClient;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            _context = context;
        }

        public async Task<List<Produto>> GetProdutosAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("produto");

                var content = await response.Content.ReadAsStringAsync();
                var jsonResult = JsonConvert.DeserializeObject(content).ToString();
                var result = JsonConvert.DeserializeObject<List<Produto>>(jsonResult);

                return result;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
