function validateInsertProfileReasonIssue() {

    // required field
    if ($('#txtInsertProfileReasonIssueErpReasonCode').val() != '') {
        $('#txtInsertProfileReasonIssueErpReasonCode').removeClass('form-control is-invalid').addClass('form-control is-valid');
    }
    else {
        $('#txtInsertProfileReasonIssueErpReasonCode').removeClass('form-control is-valid').addClass('form-control is-invalid');
    }

    if ($('#txtInsertProfileReasonIssueRdReasonCode').val() != '') {
        $('#txtInsertProfileReasonIssueRdReasonCode').removeClass('form-control is-invalid').addClass('form-control is-valid');
    }
    else {
        $('#txtInsertProfileReasonIssueRdReasonCode').removeClass('form-control is-valid').addClass('form-control is-invalid');
    }

    // Not requierd

    $('#txtInsertProfileReasonIssueDescription').removeClass('form-control is-invalid').addClass('form-control is-valid');
}


function validateUpdateProfileReasonIssue() {

    // required field
    if ($('#txtUpdateProfileReasonIssueErpReasonCode').val() != '') {
        $('#txtUpdateProfileReasonIssueErpReasonCode').removeClass('form-control is-invalid').addClass('form-control is-valid');
    }
    else {
        $('#txtUpdateProfileReasonIssueErpReasonCode').removeClass('form-control is-valid').addClass('form-control is-invalid');
    }

    if ($('#txtUpdateProfileReasonIssueRdReasonCode').val() != '') {
        $('#txtUpdateProfileReasonIssueRdReasonCode').removeClass('form-control is-invalid').addClass('form-control is-valid');
    }
    else {
        $('#txtUpdateProfileReasonIssueRdReasonCode').removeClass('form-control is-valid').addClass('form-control is-invalid');
    }

    // Not requierd

    $('#txtUpdateProfileReasonIssueDescription').removeClass('form-control is-invalid').addClass('form-control is-valid');
}