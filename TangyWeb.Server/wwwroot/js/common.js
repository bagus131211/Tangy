window.ShowToastr = (type, message) => {
    if (type === "success")
        toastr.success(message, 'Operation successful', { timeOut: 2000 });
    else if (type === "error")
        toastr.error(message, 'Operation failed', { timeOut: 2000 });
}