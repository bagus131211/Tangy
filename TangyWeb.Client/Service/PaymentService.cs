using Newtonsoft.Json;
using System.Text;
using Tangy.Models;

namespace TangyWeb.Client.Service
{
    public class PaymentService : IPaymentService
    {
        readonly HttpClient _httpClient;

        public PaymentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<SuccessDTO> Checkout(StripePaymentDTO payment)
        {
            try
            {
                string content = JsonConvert.SerializeObject(payment);
                StringContent body = new StringContent(content, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync("api/payment/create", body);
                string responseResult = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<SuccessDTO>(responseResult);
                }
                else
                {
                    var errorModel = JsonConvert.DeserializeObject<ErrorDTO>(responseResult);
                    throw new Exception(errorModel.ErrorMessage);
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
