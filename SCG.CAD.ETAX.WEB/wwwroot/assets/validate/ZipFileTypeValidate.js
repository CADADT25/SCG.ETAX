function validateInsertZipFileType() {

    if ($('#txtInsertZipFileTypeCode').val() != '') {
        $('#txtInsertZipFileTypeCode').removeClass('form-control is-invalid').addClass('form-control is-valid');
    }
    else {
        $('#txtInsertZipFileTypeCode').removeClass('form-control is-valid').addClass('form-control is-invalid');
    }


    // Not requierd

    $('#txtInsertZipFileTypeName').removeClass('form-control is-invalid').addClass('form-control is-valid');
    $('#txtInsertZipFileTypeDescription').removeClass('form-control is-invalid').addClass('form-control is-valid');

}

function validateUpdateZipFileType() {

    if ($('#txtUpdateZipFileTypeCode').val() != '') {
        $('#txtUpdateZipFileTypeCode').removeClass('form-control is-invalid').addClass('form-control is-valid');
    }
    else {
        $('#txtUpdateZipFileTypeCode').removeClass('form-control is-valid').addClass('form-control is-invalid');
    }


    // Not requierd

    $('#txtUpdateZipFileTypeName').removeClass('form-control is-invalid').addClass('form-control is-valid');
    $('#txtUpdateZipFileTypeDescription').removeClass('form-control is-invalid').addClass('form-control is-valid');

}
