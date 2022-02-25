function validateInsertProfileCertificate() {

    // required field
    if ($('#txtInsertProfileCertificatTaxNumber').val() != '') {
        $('#txtInsertProfileCertificatTaxNumber').removeClass('form-control is-invalid').addClass('form-control is-valid');
    }
    else {
        $('#txtInsertProfileCertificatTaxNumber').removeClass('form-control is-valid').addClass('form-control is-invalid');
    }

    if ($('#txtInsertProfileCertificateCompanyName').val() != '') {
        $('#txtInsertProfileCertificateCompanyName').removeClass('form-control is-invalid').addClass('form-control is-valid');
    }
    else {
        $('#txtInsertProfileCertificateCompanyName').removeClass('form-control is-valid').addClass('form-control is-invalid');
    }

    if ($('#txtInsertProfileCertificateData').val() != '') {
        $('#txtInsertProfileCertificateData').removeClass('form-control is-invalid').addClass('form-control is-valid');
    }
    else {
        $('#txtInsertProfileCertificateData').removeClass('form-control is-valid').addClass('form-control is-invalid');
    }

    if ($('#txtInsertProfileCertificateSerial').val() != '') {
        $('#txtInsertProfileCertificateSerial').removeClass('form-control is-invalid').addClass('form-control is-valid');
    }
    else {
        $('#txtInsertProfileCertificateSerial').removeClass('form-control is-valid').addClass('form-control is-invalid');
    }

    if ($('#txtInsertProfileCertificateKeyAlias').val() != '') {
        $('#txtInsertProfileCertificateKeyAlias').removeClass('form-control is-invalid').addClass('form-control is-valid');
    }
    else {
        $('#txtInsertProfileCertificateKeyAlias').removeClass('form-control is-valid').addClass('form-control is-invalid');
    }

    if ($('#txtInsertProfileCertificateStartDate').val() != '') {
        $('#txtInsertProfileCertificateStartDate').removeClass('form-control is-invalid').addClass('form-control is-valid');
    }
    else {
        $('#txtInsertProfileCertificateStartDate').removeClass('form-control is-valid').addClass('form-control is-invalid');
    }

    if ($('#txtInsertProfileCertificateEndDate').val() != '') {
        $('#txtInsertProfileCertificateData').removeClass('form-control is-invalid').addClass('form-control is-valid');
    }
    else {
        $('#txtInsertProfileCertificateEndDate').removeClass('form-control is-valid').addClass('form-control is-invalid');
    }

    // Not requierd
}


function validateUpdateProfileCertificate() {

    // required field
    if ($('#txtUpdateProfileCertificatTaxNumber').val() != '') {
        $('#txtUpdateProfileCertificatTaxNumber').removeClass('form-control is-invalid').addClass('form-control is-valid');
    }
    else {
        $('#txtUpdateProfileCertificatTaxNumber').removeClass('form-control is-valid').addClass('form-control is-invalid');
    }

    if ($('#txtUpdateProfileCertificateCompanyName').val() != '') {
        $('#txtUpdateProfileCertificateCompanyName').removeClass('form-control is-invalid').addClass('form-control is-valid');
    }
    else {
        $('#txtUpdateProfileCertificateCompanyName').removeClass('form-control is-valid').addClass('form-control is-invalid');
    }

    if ($('#txtUpdateProfileCertificateData').val() != '') {
        $('#txtUpdateProfileCertificateData').removeClass('form-control is-invalid').addClass('form-control is-valid');
    }
    else {
        $('#txtUpdateProfileCertificateData').removeClass('form-control is-valid').addClass('form-control is-invalid');
    }

    if ($('#txtUpdateProfileCertificateSerial').val() != '') {
        $('#txtUpdateProfileCertificateSerial').removeClass('form-control is-invalid').addClass('form-control is-valid');
    }
    else {
        $('#txtUpdateProfileCertificateSerial').removeClass('form-control is-valid').addClass('form-control is-invalid');
    }

    if ($('#txtUpdateProfileCertificateKeyAlias').val() != '') {
        $('#txtUpdateProfileCertificateKeyAlias').removeClass('form-control is-invalid').addClass('form-control is-valid');
    }
    else {
        $('#txtUpdateProfileCertificateKeyAlias').removeClass('form-control is-valid').addClass('form-control is-invalid');
    }

    if ($('#txtUpdateProfileCertificateStartDate').val() != '') {
        $('#txtUpdateProfileCertificateStartDate').removeClass('form-control is-invalid').addClass('form-control is-valid');
    }
    else {
        $('#txtUpdateProfileCertificateStartDate').removeClass('form-control is-valid').addClass('form-control is-invalid');
    }

    if ($('#txtUpdateProfileCertificateEndDate').val() != '') {
        $('#txtUpdateProfileCertificateData').removeClass('form-control is-invalid').addClass('form-control is-valid');
    }
    else {
        $('#txtUpdateProfileCertificateEndDate').removeClass('form-control is-valid').addClass('form-control is-invalid');
    }

}