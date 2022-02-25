function validateInsertProfileEmailType() {

    if ($('#txtInsertProfileEmailTypeCode').val() != '') {
        $('#txtInsertProfileEmailTypeCode').removeClass('form-control is-invalid').addClass('form-control is-valid');
    }
    else {
        $('#txtInsertProfileEmailTypeCode').removeClass('form-control is-valid').addClass('form-control is-invalid');
    }


    // Not requierd

    $('#txtInsertProfileEmailTypeName').removeClass('form-control is-invalid').addClass('form-control is-valid');
    $('#txtInsertProfileEmailTypeDescription').removeClass('form-control is-invalid').addClass('form-control is-valid');

}

function validateUpdateProfileEmailType() {

    if ($('#txtUpdateProfileEmailTypeCode').val() != '') {
        $('#txtUpdateProfileEmailTypeCode').removeClass('form-control is-invalid').addClass('form-control is-valid');
    }
    else {
        $('#txtUpdateProfileEmailTypeCode').removeClass('form-control is-valid').addClass('form-control is-invalid');
    }


    // Not requierd

    $('#txtUpdateProfileEmailTypeName').removeClass('form-control is-invalid').addClass('form-control is-valid');
    $('#txtUpdateProfileEmailTypeDescription').removeClass('form-control is-invalid').addClass('form-control is-valid');

}
