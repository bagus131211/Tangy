window.ShowToastr = (type, message) => {
    if (type === "success")
        toastr.success(message, 'Operation successful', { timeOut: 2000 });
    else if (type === "error")
        toastr.error(message, 'Operation failed', { timeOut: 2000 });
};

window.ShowSwal = (type, message) => {
    if (type === "success")
        Swal.fire("Success Notification!", message, "success");
    if (type === "error")
        Swal.fire("Error Notification!!!", message, "error");
};

showDeleteConfirmationModal = () => {
    $('#confirm_modal').modal('show');
};

hideDeleteConfirmationModal = () => {
    $('#confirm_modal').modal('hide');
};