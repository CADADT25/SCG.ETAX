function validateInsertProfileCompanyCode() {

    if ($('#txtInsertProfileCompanyCode').val() != '') {
        $('#txtInsertProfileCompanyCode').removeClass('form-control is-invalid').addClass('form-control is-valid');
    }
    else {
        $('#txtInsertProfileCompanyCode').removeClass('form-control is-valid').addClass('form-control is-invalid');
    }


    // Not requierd

    $('#txtInsertProfileCompanyCodeDescription').removeClass('form-control is-invalid').addClass('form-control is-valid');

}


function validateUpdateProfileCompanyCode() {

    // required field
    if ($('#txtUpdateProfileCompanyCode').val() != '') {
        $('#txtUpdateProfileCompanyCode').removeClass('form-control is-invalid').addClass('form-control is-valid');
    }
    else {
        $('#txtUpdateProfileCompanyCode').removeClass('form-control is-valid').addClass('form-control is-invalid');
    }


    // Not requierd

    $('#txtUpdateProfileCompanyCodeDescription').removeClass('form-control is-invalid').addClass('form-control is-valid');
}