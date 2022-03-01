function validateInsertProfileEmailTemplate() {

    // required field
    if ($('#txtInsertProfileEmailTemplateEmailBody').val() != '') {
        $('#txtInsertProfileEmailTemplateEmailBody').removeClass('form-control is-invalid').addClass('form-control is-valid');
    }
    else {
        $('#txtInsertProfileEmailTemplateEmailBody').removeClass('form-control is-valid').addClass('form-control is-invalid');
    }

}


function validateUpdateProfileEmailTemplate() {

    // required field
    if ($('#txtUpdateProfileEmailTemplateEmailBody').val() != '') {
        $('#txtUpdateProfileEmailTemplateEmailBody').removeClass('form-control is-invalid').addClass('form-control is-valid');
    }
    else {
        $('#txtUpdateProfileEmailTemplateEmailBody').removeClass('form-control is-valid').addClass('form-control is-invalid');
    }

}