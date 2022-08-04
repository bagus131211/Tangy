namespace TangyWeb.Client.Pages
{
    public partial class Logout
    {
        protected override async Task OnInitializedAsync()
        {
            await _authService.Logout();
            //with forceload
            //_navigation.NavigateTo("/", true);
            _navigation.NavigateTo("/");
        }
    }
}
