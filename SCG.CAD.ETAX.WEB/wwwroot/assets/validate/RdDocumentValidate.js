function validateInsertRdDocument() {

    // required field
    if ($('#txtInsertRdDocumentCode').val() != '') {
        $('#txtInsertRdDocumentCode').removeClass('form-control is-invalid').addClass('form-control is-valid');
    }
    else {
        $('#txtInsertRdDocumentCode').removeClass('form-control is-valid').addClass('form-control is-invalid');
    }

    if ($('#txtInsertRdDocumentNameTH').val() != '') {
        $('#txtInsertRdDocumentNameTH').removeClass('form-control is-invalid').addClass('form-control is-valid');
    }
    else {
        $('#txtInsertRdDocumentNameTH').removeClass('form-control is-valid').addClass('form-control is-invalid');
    }


    // Not requierd

    $('#txtInsertRdDocumentNameEN').removeClass('form-control is-invalid').addClass('form-control is-valid');
}


function validateUpdateRdDocument() {

    // required field
    if ($('#txtUpdateRdDocumentCode').val() != '') {
        $('#txtUpdateRdDocumentCode').removeClass('form-control is-invalid').addClass('form-control is-valid');
    }
    else {
        $('#txtUpdateRdDocumentCode').removeClass('form-control is-valid').addClass('form-control is-invalid');
    }

    if ($('#txtUpdateRdDocumentNameTH').val() != '') {
        $('#txtUpdateRdDocumentNameTH').removeClass('form-control is-invalid').addClass('form-control is-valid');
    }
    else {
        $('#txtUpdateRdDocumentNameTH').removeClass('form-control is-valid').addClass('form-control is-invalid');
    }

    // Not requierd

    $('#txtUpdateRdDocumentNameEN').removeClass('form-control is-invalid').addClass('form-control is-valid');
}