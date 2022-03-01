function validateInsertZipFileConfig() {

    if ($('#txtInsertZipFileConfigName1').val() != '') {
        $('#txtInsertZipFileConfigName1').removeClass('form-control is-invalid').addClass('form-control is-valid');
    }
    else {
        $('#txtInsertZipFileConfigName1').removeClass('form-control is-valid').addClass('form-control is-invalid');
    }

    if ($('#txtInsertZipFileConfigValue1').val() != '') {
        $('#txtInsertZipFileConfigValue1').removeClass('form-control is-invalid').addClass('form-control is-valid');
    }
    else {
        $('#txtInsertZipFileConfigValue1').removeClass('form-control is-valid').addClass('form-control is-invalid');
    }

    // Not requierd

    $('#txtInsertZipFileConfigName2').removeClass('form-control is-invalid').addClass('form-control is-valid');
    $('#txtInsertZipFileConfigValue2').removeClass('form-control is-invalid').addClass('form-control is-valid');
    $('#txtInsertZipFileConfigName3').removeClass('form-control is-invalid').addClass('form-control is-valid');
    $('#txtInsertZipFileConfigValue3').removeClass('form-control is-invalid').addClass('form-control is-valid');
    $('#txtInsertZipFileConfigName4').removeClass('form-control is-invalid').addClass('form-control is-valid');
    $('#txtInsertZipFileConfigValue4').removeClass('form-control is-invalid').addClass('form-control is-valid');
    $('#txtInsertZipFileConfigName5').removeClass('form-control is-invalid').addClass('form-control is-valid');
    $('#txtInsertZipFileConfigValue5').removeClass('form-control is-invalid').addClass('form-control is-valid');

}

function validateUpdateZipFileConfig() {


    if ($('#txtUpdateZipFileConfigName1').val() != '') {
        $('#txtUpdateZipFileConfigName1').removeClass('form-control is-invalid').addClass('form-control is-valid');
    }
    else {
        $('#txtUpdateZipFileConfigName1').removeClass('form-control is-valid').addClass('form-control is-invalid');
    }

    if ($('#txtUpdateZipFileConfigValue1').val() != '') {
        $('#txtUpdateZipFileConfigValue1').removeClass('form-control is-invalid').addClass('form-control is-valid');
    }
    else {
        $('#txtUpdateZipFileConfigValue1').removeClass('form-control is-valid').addClass('form-control is-invalid');
    }

    // Not requierd

    $('#txtUpdateZipFileConfigName2').removeClass('form-control is-invalid').addClass('form-control is-valid');
    $('#txtUpdateZipFileConfigValue2').removeClass('form-control is-invalid').addClass('form-control is-valid');
    $('#txtUpdateZipFileConfigName3').removeClass('form-control is-invalid').addClass('form-control is-valid');
    $('#txtUpdateZipFileConfigValue3').removeClass('form-control is-invalid').addClass('form-control is-valid');
    $('#txtUpdateZipFileConfigName4').removeClass('form-control is-invalid').addClass('form-control is-valid');
    $('#txtUpdateZipFileConfigValue4').removeClass('form-control is-invalid').addClass('form-control is-valid');
    $('#txtUpdateZipFileConfigName5').removeClass('form-control is-invalid').addClass('form-control is-valid');
    $('#txtUpdateZipFileConfigValue5').removeClass('form-control is-invalid').addClass('form-control is-valid');

}
