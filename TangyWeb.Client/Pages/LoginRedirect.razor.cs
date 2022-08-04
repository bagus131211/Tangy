using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace TangyWeb.Client.Pages
{
    public partial class LoginRedirect
    {
        bool _isNotAuthorized = default;

        [CascadingParameter]
        public Task<AuthenticationState> State { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var authState = await State;
            if (authState?.User.Identity is null || authState?.User?.Identity?.IsAuthenticated is false)
            {
                var currentUri = _navigation.ToBaseRelativePath(_navigation.Uri);
                if (string.IsNullOrEmpty(currentUri))
                {
                    _navigation.NavigateTo("login");
                }
                _navigation.NavigateTo($"login?returnUrl={currentUri}");
            }
            _isNotAuthorized = true;
        }
    }
}
