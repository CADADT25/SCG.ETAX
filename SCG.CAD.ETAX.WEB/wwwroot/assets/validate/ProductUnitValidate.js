function validateInsertProductUnit() {

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
}


function validateUpdateProductUnit() {

    // required field
    if ($('#txtUpdateProductUnitErp').val() != '') {
        $('#txtUpdateProductUnitErp').removeClass('form-control is-invalid').addClass('form-control is-valid');
    }
    else {
        $('#txtUpdateProductUnitErp').removeClass('form-control is-valid').addClass('form-control is-invalid');
    }

    if ($('#txtUpdateProductUnitRd').val() != '') {
        $('#txtUpdateProductUnitRd').removeClass('form-control is-invalid').addClass('form-control is-valid');
    }
    else {
        $('#txtUpdateProductUnitRd').removeClass('form-control is-valid').addClass('form-control is-invalid');
    }

    // Not requierd

    $('#txtUpdatetProductUnitDescription').removeClass('form-control is-invalid').addClass('form-control is-valid');
}