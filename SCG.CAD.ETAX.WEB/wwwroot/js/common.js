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
        title: 'Error',
        text: error
        //text: 'Something went wrong!',
        //footer: '<a href="javascript:void();">' + error + '</a>'
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

function fnAlertValidate(str) {
    Swal.fire({
        title: 'Warning',
        text: str,
        icon: 'warning',
        customClass: {
            confirmButton: 'btn btn-primary'
        }
    });
}

function fnSweetAlert_SessionExpire() {

    Swal.fire({
        icon: 'warning',
        title: 'Warning',
        text: 'Session expired',
        showConfirmButton: false,
        footer: '<a href="/AuthSinIn/Index">Login</a>'
    });
}


function csvToArray(str, delimiter = ",") {
    // slice from start of text to the first \n index
    // use split to create an array from string by delimiter
    const headers = str.slice(0, str.indexOf("\n")).split(delimiter);

    // slice from \n index + 1 to the end of the text
    // use split to create an array of each csv value row
    const rows = str.slice(str.indexOf("\n") + 1).split("\n");

    // Map the rows
    // split values from each row into an array
    // use headers.reduce to create an object
    // object properties derived from headers:values
    // the object passed as an element of the array
    const arr = rows.map(function (row) {
        const values = row.split(delimiter);
        const el = headers.reduce(function (object, header, index) {
            object[header] = values[index];
            return object;
        }, {});
        if (el.CustomerId != null && el.CustomerId != undefined) {
            return el;
        }
    });

    // return the array
    return arr;
}

