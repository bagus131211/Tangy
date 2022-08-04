using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using Tangy.Common;
using Tangy.Models;

namespace TangyWeb.Client.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        readonly ILocalStorageService _localStorage;
        readonly HttpClient _httpClient;
        readonly AuthenticationStateProvider _authenticationStateProvider;

        public AuthenticationService(
            ILocalStorageService localStorage, 
            HttpClient httpClient, 
            AuthenticationStateProvider authenticationServiceProvider)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _authenticationStateProvider = authenticationServiceProvider;
        }
        public async Task<SignInResponseDTO> Login(SignInRequestDTO signInRequest)
        {
            var content = JsonConvert.SerializeObject(signInRequest);
            var body = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/account/signin", body);
            var temporaryContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<SignInResponseDTO>(temporaryContent);

            if (response.IsSuccessStatusCode)
            {
                await _localStorage.SetItemAsync(Constants.Authentication.Token, result.Token);
                await _localStorage.SetItemAsync(Constants.Authentication.UserDetails, result.User);
                ((AuthenticationStateProviderExtension)_authenticationStateProvider).NotifyUserSignedIn(result.Token);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.Token);
                return new SignInResponseDTO
                {
                    IsSuccess = true
                };
            }
            return result;
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync(Constants.Authentication.Token);
            await _localStorage.RemoveItemAsync(Constants.Authentication.UserDetails);
            ((AuthenticationStateProviderExtension)_authenticationStateProvider).NotifyUserSignedOut();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<SignUpResponseDTO> RegisterUser(SignUpRequestDTO signUpRequest)
        {
            var content = JsonConvert.SerializeObject(signUpRequest);
            var body = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/account/signup", body);
            var temporaryContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<SignUpResponseDTO>(temporaryContent);

            if (response.IsSuccessStatusCode)
            {
                return new SignUpResponseDTO { IsSignUpSuccessful = true };
            }
            
            return new SignUpResponseDTO { IsSignUpSuccessful = false, Errors = result.Errors };
        }
    }
}
