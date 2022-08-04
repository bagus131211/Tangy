using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Security.Claims;
using Tangy.Common;
using TangyWeb.Client.Helper;

namespace TangyWeb.Client.Service
{
    public class AuthenticationStateProviderExtension : AuthenticationStateProvider
    {
        readonly HttpClient _httpClient;
        readonly ILocalStorageService _localStorage;

        public AuthenticationStateProviderExtension(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _localStorage.GetItemAsync<string>(Constants.Authentication.Token);
            if (token == null)
                //even if token is null it is still created the identity as default
                //return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(new []
                //{
                //    new Claim(ClaimTypes.Name, "tokenNull@gmail.com")
                //},"jwtAuthType")));
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(JwtParser.ParseClaimFromJwt(token), "jwtAuthType")));
        }

        public void NotifyUserSignedIn(string token) => NotifyAuthenticationStateChanged(SetState(JwtParser.ParseClaimFromJwt(token)));        

        public void NotifyUserSignedOut() => NotifyAuthenticationStateChanged(SetState());        

        async Task<AuthenticationState> SetState(dynamic param = null)
        {
            if (param == null)
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            else
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(param, "jwtAuthType")));
        }
    }
}
