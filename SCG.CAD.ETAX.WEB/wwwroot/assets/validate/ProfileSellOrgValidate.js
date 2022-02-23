function validateInsertProfileSellOrg() {

    // required field
    if ($('#txtInsertProfileSellOrgCode').val() != '') {
        $('#txtInsertProfileSellOrgCode').removeClass('form-control is-invalid').addClass('form-control is-valid');
    }
    else {
        $('#txtInsertProfileSellOrgCode').removeClass('form-control is-valid').addClass('form-control is-invalid');
    }

    // Not requierd

    $('#txtInsertProfileSellOrgDescription').removeClass('form-control is-invalid').addClass('form-control is-valid');

}


function validateUpdateProfileSellOrg() {

    // required field
    if ($('#txtUpdateProfileSellOrgCode').val() != '') {
        $('#txtUpdateProfileSellOrgCode').removeClass('form-control is-invalid').addClass('form-control is-valid');
    }
    else {
        $('#txtUpdateProfileSellOrgCode').removeClass('form-control is-valid').addClass('form-control is-invalid');
    }

    // Not requierd

    $('#txtUpdateProfileSellOrgDescription').removeClass('form-control is-invalid').addClass('form-control is-valid');


}