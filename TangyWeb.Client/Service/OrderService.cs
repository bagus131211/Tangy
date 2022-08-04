using Newtonsoft.Json;
using System.Text;
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

        public async Task<OrderDTO> Create(StripePaymentDTO payment)
        {
            var content = JsonConvert.SerializeObject(payment);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/order/create", bodyContent);
            var tempContent = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<OrderDTO>(tempContent);
                return result;
            }
            return new OrderDTO();

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

        public async Task<OrderHeaderDTO> MarkPaymentSuccessful(OrderHeaderDTO orderHeader)
        {
            var content = JsonConvert.SerializeObject(orderHeader);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/order/markpayment", bodyContent);
            var tempContent = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<OrderHeaderDTO>(tempContent);
                return result;
            }
            var error = JsonConvert.DeserializeObject<ErrorDTO>(tempContent);
            throw new Exception(error.ErrorMessage);
        }
    }
}

