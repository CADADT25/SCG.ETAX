function fnToastrInputDataFail() {
    toastr.warning('Please check input your data and try again !', { "showDuration": 500 });
}




function fnSweetAlert_SaveSuccess() {
    Swal.fire({
        title: 'Good job!',
        text: 'Your has been saved!',
        icon: 'success',
        customClass: {
            confirmButton: 'btn btn-primary'
        },
        buttonsStyling: false,
        showConfirmButton: false,
        timer: 1000
    });
}

function fnSweetAlert_DeleteSuccess() {
    Swal.fire({
        title: 'Good job!',
        text: 'Your has been deleted!',
        icon: 'success',
        customClass: {
            confirmButton: 'btn btn-primary'
        },
        buttonsStyling: false
    });
}

function fnSweetAlert_Error(error) {
    Swal.fire({
        icon: 'error',
        title: 'Oops...',
        text: 'Something went wrong!',
        footer: '<a href="javascript:void();">' + error + '</a>'
    });
}

function fnSweetAlert_WarningSelectItem() {
    Swal.fire({
        title: 'Warning',
        text: 'Please select item.',
        icon: 'warning',
        customClass: {
            confirmButton: 'btn btn-primary'
        }
    });
}

function fnSweetAlert_Success() {
    Swal.fire({
        title: 'Good job!',
        text: 'Your has been saved!',
        icon: 'success',
        customClass: {
            confirmButton: 'btn btn-primary'
        }
    });
}

