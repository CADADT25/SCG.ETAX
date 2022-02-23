function validateInsertDataSource() {

    // required field
    if ($('#txtInsertDataSourceName').val() != '') {
        $('#txtInsertDataSourceName').removeClass('form-control is-invalid').addClass('form-control is-valid');
    }
    else {
        $('#txtInsertDataSourceName').removeClass('form-control is-valid').addClass('form-control is-invalid');
    }


    // Not requierd

    $('#txtInsertDataSourceDescription').removeClass('form-control is-invalid').addClass('form-control is-valid');
}


function validateUpdateDataSource() {

    // required field
    if ($('#txtUpdateDataSourceName').val() != '') {
        $('#txtUpdateDataSourceName').removeClass('form-control is-invalid').addClass('form-control is-valid');
    }
    else {
        $('#txtUpdateDataSourceName').removeClass('form-control is-valid').addClass('form-control is-invalid');
    }

    // Not requierd

    $('#txtUpdateDataSourceDescription').removeClass('form-control is-invalid').addClass('form-control is-valid');
}