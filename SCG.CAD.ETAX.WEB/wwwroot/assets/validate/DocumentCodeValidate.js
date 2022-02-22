function validateInsertDocumentCode() {

    // required field
    if ($('#txtInsertDocumentCodeErpSource').val() != '') {
        $('#txtInsertDocumentCodeErpSource').removeClass('form-control is-invalid').addClass('form-control is-valid');
    }
    else {
        $('#txtInsertDocumentCodeErpSource').removeClass('form-control is-valid').addClass('form-control is-invalid');
    }

    if ($('#txtInsertDocumentCodeErp').val() != '') {
        $('#txtInsertDocumentCodeErp').removeClass('form-control is-invalid').addClass('form-control is-valid');
    }
    else {
        $('#txtInsertDocumentCodeErp').removeClass('form-control is-valid').addClass('form-control is-invalid');
    }

    if ($('#txtInsertDocumentCodeRd').val() != '') {
        $('#txtInsertDocumentCodeRd').removeClass('form-control is-invalid').addClass('form-control is-valid');
    }
    else {
        $('#txtInsertDocumentCodeRd').removeClass('form-control is-valid').addClass('form-control is-invalid');
    }


    // Not requierd

    $('#txtInsertDocumentCodeDescription').removeClass('form-control is-invalid').addClass('form-control is-valid');
}


function validateUpdateDocumentCode() {

    // required field
    if ($('#txtUpdateDocumentCodeErpSource').val() != '') {
        $('#txtUpdateDocumentCodeErpSource').removeClass('form-control is-invalid').addClass('form-control is-valid');
    }
    else {
        $('#txtUpdateDocumentCodeErpSource').removeClass('form-control is-valid').addClass('form-control is-invalid');
    }

    if ($('#txtUpdateDocumentCodeErp').val() != '') {
        $('#txtUpdateDocumentCodeErp').removeClass('form-control is-invalid').addClass('form-control is-valid');
    }
    else {
        $('#txtUpdateDocumentCodeErp').removeClass('form-control is-valid').addClass('form-control is-invalid');
    }

    if ($('#txtUpdateDocumentCodeRd').val() != '') {
        $('#txtUpdateDocumentCodeRd').removeClass('form-control is-invalid').addClass('form-control is-valid');
    }
    else {
        $('#txtUpdateDocumentCodeRd').removeClass('form-control is-valid').addClass('form-control is-invalid');
    }

    // Not requierd

    $('#txtUpdateDocumentCodeDescription').removeClass('form-control is-invalid').addClass('form-control is-valid');
}