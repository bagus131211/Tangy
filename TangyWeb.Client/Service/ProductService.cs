using Newtonsoft.Json;
using Tangy.Models;

namespace TangyWeb.Client.Service
{
    public class ProductService : IProductService
    {
        readonly HttpClient _httpClient;
        IConfiguration _configuration;
        string _baseServerUri;

        public ProductService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _baseServerUri = _configuration.GetSection("BaseServerUri").Value;
        }
        public async Task<IEnumerable<ProductDTO>> GetAll()
        {
            var response = await _httpClient.GetAsync("/api/product");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var products = JsonConvert.DeserializeObject<IEnumerable<ProductDTO>>(content);
                foreach (var product in products)
                {
                    product.ImageUri = _baseServerUri + product.ImageUri;
                }
                return products;
            }
            return new List<ProductDTO>();
        }

        public async Task<ProductDTO> GetById(int id)
        {
            var response = await _httpClient.GetAsync($"/api/product/{id}");
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var product = JsonConvert.DeserializeObject<ProductDTO>(content);
                product.ImageUri = _baseServerUri + product.ImageUri;
                return product;
            }
            else
            {
                var error = JsonConvert.DeserializeObject<ErrorDTO>(content);
                throw new Exception(error.ErrorMessage);
            }
        }
    }
}
