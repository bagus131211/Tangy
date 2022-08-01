﻿using Microsoft.JSInterop;

namespace TangyWeb.Client.Helper
{
    public static class JsRuntimeExtension
    {
        public static async ValueTask ToastrSuccess(this IJSRuntime jsRuntime, string message) 
            => await jsRuntime.InvokeVoidAsync("ShowToastr", "success", message);

        public static async ValueTask ToastrFailure(this IJSRuntime jsRuntime, string message)
            => await jsRuntime.InvokeVoidAsync("ShowToastr", "error", message);

        public static async ValueTask SwalSuccess(this IJSRuntime jsRuntime, string message)
            => await jsRuntime.InvokeVoidAsync("ShowSwal", "success", message);

        public static async ValueTask SwalFailure(this IJSRuntime jsRuntime, string message)
            => await jsRuntime.InvokeVoidAsync("ShowSwal", "error", message);

    }
}