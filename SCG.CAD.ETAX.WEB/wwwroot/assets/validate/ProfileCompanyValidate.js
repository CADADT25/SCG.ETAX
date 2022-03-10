function validateInsertProfileCompany() {

    if ($('#txtInsertProfileCompanyCode').val() != '') {
        $('#txtInsertProfileCompanyCode').removeClass('form-control is-invalid').addClass('form-control is-valid');
    }
    else {
        $('#txtInsertProfileCompanyCode').removeClass('form-control is-valid').addClass('form-control is-invalid');
    }

    if ($('#txtInsertProfileCompanyNameTh').val() != '') {
        $('#txtInsertProfileCompanyNameTh').removeClass('form-control is-invalid').addClass('form-control is-valid');
    }
    else {
        $('#txtInsertProfileCompanyNameTh').removeClass('form-control is-valid').addClass('form-control is-invalid');
    }

    if ($('#txtInsertProfileCompanyNameEn').val() != '') {
        $('#txtInsertProfileCompanyNameEn').removeClass('form-control is-invalid').addClass('form-control is-valid');
    }
    else {
        $('#txtInsertProfileCompanyNameEn').removeClass('form-control is-valid').addClass('form-control is-invalid');
    }
    if ($('#txtInsertProfileCompanyTaxNumber').val() != '') {
        $('#txtInsertProfileCompanyTaxNumber').removeClass('form-control is-invalid').addClass('form-control is-valid');
    }
    else {
        $('#txtInsertProfileCompanyTaxNumber').removeClass('form-control is-valid').addClass('form-control is-invalid');
    }


}


function validateUpdateProfileCompany() {


    if ($('#txtUpdateProfileCompanyCode').val() != '') {
        $('#txtUpdateProfileCompanyCode').removeClass('form-control is-invalid').addClass('form-control is-valid');
    }
    else {
        $('#txtUpdateProfileCompanyCode').removeClass('form-control is-valid').addClass('form-control is-invalid');
    }

    if ($('#txtUpdateProfileCompanyNameTh').val() != '') {
        $('#txtUpdateProfileCompanyNameTh').removeClass('form-control is-invalid').addClass('form-control is-valid');
    }
    else {
        $('#txtUpdateProfileCompanyNameTh').removeClass('form-control is-valid').addClass('form-control is-invalid');
    }

    if ($('#txtUpdateProfileCompanyNameEn').val() != '') {
        $('#txtUpdateProfileCompanyNameEn').removeClass('form-control is-invalid').addClass('form-control is-valid');
    }
    else {
        $('#txtUpdateProfileCompanyNameEn').removeClass('form-control is-valid').addClass('form-control is-invalid');
    }
    if ($('#txtUpdateProfileCompanyTaxNumber').val() != '') {
        $('#txtUpdateProfileCompanyTaxNumber').removeClass('form-control is-invalid').addClass('form-control is-valid');
    }
    else {
        $('#txtUpdateProfileCompanyTaxNumber').removeClass('form-control is-valid').addClass('form-control is-invalid');
    }


}