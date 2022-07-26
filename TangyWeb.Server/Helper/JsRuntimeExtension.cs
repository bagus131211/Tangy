using Microsoft.JSInterop;

namespace TangyWeb.Server.Helper
{
    public static class JsRuntimeExtension
    {
        public static async ValueTask ToastrSuccess(this IJSRuntime jsRuntime, string message) 
            => await jsRuntime.InvokeVoidAsync("ShowToastr", "success", message);

        public static async ValueTask ToastrFailure(this IJSRuntime jsRuntime, string message)
            => await jsRuntime.InvokeVoidAsync("ShowToastr", "error", message);

    }
}
