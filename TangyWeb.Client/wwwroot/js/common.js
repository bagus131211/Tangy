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

redirectToCheckout = (sessionId) => {
    var stripe = Stripe("pk_test_51LT1r9CdXMeNGCgoLsfyRkU2IX26GGwwh96RF5fNt0PUZlxKT4aPMxVt4ywvQqSZFuX1k4n3JMRX5M06jLZQNurC008jDg8GXn");
    stripe.redirectToCheckout({ sessionId: sessionId });
}