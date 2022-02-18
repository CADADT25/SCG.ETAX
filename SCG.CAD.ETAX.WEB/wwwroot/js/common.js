function fnSweetAlert_SaveSuccess() {
    Swal.fire({
        type: 'success',
        title: 'Your work has been saved',
        showConfirmButton: true
    });
}

function fnSweetAlert_DeleteSuccess() {
    Swal.fire({
        type: 'success',
        title: 'Your work has been deleted',
        showConfirmButton: true
    });
}

function fnSweetAlert_Error() {
    Swal.fire({
        type: 'error',
        title: 'Oops...',
        text: 'Something went wrong!',
        footer: '<a href="javascript:void();">Why do I have this issue?</a>'
    });
}

