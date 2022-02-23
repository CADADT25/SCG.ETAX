function validateInsertErpDocument() {

    // required field
    if ($('#txtInsertErpDocumentCode').val() != '') {
        $('#txtInsertErpDocumentCode').removeClass('form-control is-invalid').addClass('form-control is-valid');
    }
    else {
        $('#txtInsertErpDocumentCode').removeClass('form-control is-valid').addClass('form-control is-invalid');
    }

    

    // Not requierd

    $('#txtInsertErpDocumentNameTh').removeClass('form-control is-invalid').addClass('form-control is-valid');
    $('#txtInsertErpDocumentNameEn').removeClass('form-control is-invalid').addClass('form-control is-valid');

}


function validateUpdateErpDocument() {

    // required field
    if ($('#txtUpdateErpDocumentCode').val() != '') {
        $('#txtUpdateErpDocumentCode').removeClass('form-control is-invalid').addClass('form-control is-valid');
    }
    else {
        $('#txtUpdateErpDocumentCode').removeClass('form-control is-valid').addClass('form-control is-invalid');
    }



    // Not requierd

    $('#txtUpdateErpDocumentNameTh').removeClass('form-control is-invalid').addClass('form-control is-valid');
    $('#txtUpdateErpDocumentNameEn').removeClass('form-control is-invalid').addClass('form-control is-valid');


}