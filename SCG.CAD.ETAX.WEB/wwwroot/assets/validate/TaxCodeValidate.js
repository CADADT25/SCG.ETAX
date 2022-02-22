function validateInsertTaxCode() {

    // required field
    if ($('#txtInsertTaxCodeErp').val() != '') {
        $('#txtInsertTaxCodeErp').removeClass('form-control is-invalid').addClass('form-control is-valid');
    }
    else {
        $('#txtInsertTaxCodeErp').removeClass('form-control is-valid').addClass('form-control is-invalid');
    }

    if ($('#txtInsertTaxCodeRd').val() != '') {
        $('#txtInsertTaxCodeRd').removeClass('form-control is-invalid').addClass('form-control is-valid');
    }
    else {
        $('#txtInsertTaxCodeRd').removeClass('form-control is-valid').addClass('form-control is-invalid');
    }

    // Not requierd

    $('#txtInsertTaxCodeDescription').removeClass('form-control is-invalid').addClass('form-control is-valid');
}


function validateUpdateTaxCode() {

    // required field
    if ($('#txtUpdateTaxCodeErp').val() != '') {
        $('#txtUpdateTaxCodeErp').removeClass('form-control is-invalid').addClass('form-control is-valid');
    }
    else {
        $('#txtUpdateTaxCodeErp').removeClass('form-control is-valid').addClass('form-control is-invalid');
    }

    if ($('#txtUpdateTaxCodeRd').val() != '') {
        $('#txtUpdateTaxCodeRd').removeClass('form-control is-invalid').addClass('form-control is-valid');
    }
    else {
        $('#txtUpdateTaxCodeRd').removeClass('form-control is-valid').addClass('form-control is-invalid');
    }

    // Not requierd

    $('#txtUpdatetTaxCodeDescription').removeClass('form-control is-invalid').addClass('form-control is-valid');
}