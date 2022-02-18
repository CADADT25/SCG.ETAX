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
}