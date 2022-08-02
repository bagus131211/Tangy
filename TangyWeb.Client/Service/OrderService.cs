using Newtonsoft.Json;
using Tangy.Models;

namespace TangyWeb.Client.Service
{
    public class OrderService : IOrderService
    {
        readonly HttpClient _httpClient;
        IConfiguration _configuration;
        string _baseServerUri;

        public OrderService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _baseServerUri = _configuration.GetSection("BaseServerUri").Value;
        }
        public async Task<ICollection<OrderDTO>> GetAll(string? userId)
        {
            var response = await _httpClient.GetAsync("/api/order");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var orders = JsonConvert.DeserializeObject<ICollection<OrderDTO>>(content);
                return orders;
            }
            return new List<OrderDTO>();
        }

        public async Task<OrderDTO> GetById(int id)
        {
            var response = await _httpClient.GetAsync($"/api/order/{id}");
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var order = JsonConvert.DeserializeObject<OrderDTO>(content);
                return order;
            }
            else
            {
                var error = JsonConvert.DeserializeObject<ErrorDTO>(content);
                throw new Exception(error.ErrorMessage);
            }
        }
    }
}

