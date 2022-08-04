using Microsoft.AspNetCore.Components;
using Tangy.Models;

namespace TangyWeb.Client.Pages
{
    using Service;
    using System.Web;

    public partial class Login
    {
        SignInRequestDTO _signInRequest = new();
        string _error = string.Empty;
        bool _showLoginError;
        bool _isProcessing = default;
        string _returnUrl;

        [Inject]
        public IAuthenticationService _authService { get; set; }
        [Inject]
        public NavigationManager _navigation { get; set; }

        async Task UserLogin()
        {
            _showLoginError = false;
            _isProcessing = true;
            var result = await _authService.Login(_signInRequest);
            if (result.IsSuccess)
            {
                var absoluteUri = new Uri(_navigation.Uri);
                var queryParam = HttpUtility.ParseQueryString(absoluteUri.Query);
                _returnUrl = queryParam["returnUrl"];
                //with forceload
                //_navigation.NavigateTo("/", true);
                if (string.IsNullOrEmpty(_returnUrl))
                    _navigation.NavigateTo("/");
                else
                    _navigation.NavigateTo("/" + _returnUrl);               
            }
            else
            {
                _error = result.ErrorMessage;
                _showLoginError = true;
            }
            _isProcessing = false;            
        }
    }
}
