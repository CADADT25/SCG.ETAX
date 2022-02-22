function validateProductUnit() {

    // required field
    if ($('#txtInsertProductUnitErp').val() != '') {
        $('#txtInsertProductUnitErp').removeClass('form-control is-invalid').addClass('form-control is-valid');
    }
    else {
        $('#txtInsertProductUnitErp').removeClass('form-control is-valid').addClass('form-control is-invalid');
    }

    if ($('#txtInsertProductUnitRd').val() != '') {
        $('#txtInsertProductUnitRd').removeClass('form-control is-invalid').addClass('form-control is-valid');
    }
    else {
        $('#txtInsertProductUnitRd').removeClass('form-control is-valid').addClass('form-control is-invalid');
    }

    // Not requierd

    $('#txtInsertProductUnitDescription').removeClass('form-control is-invalid').addClass('form-control is-valid');

    //============================

    
}


function validateTaxCode() {

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



function validateRdDocument() {

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


function validateDocumentCode() {

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