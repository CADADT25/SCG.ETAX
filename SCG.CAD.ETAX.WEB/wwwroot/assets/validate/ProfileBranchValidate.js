function validateInsertProfileBranch() {

    if ($('#txtInsertProfileBranchCode').val() != '') {
        $('#txtInsertProfileBranchCode').removeClass('form-control is-invalid').addClass('form-control is-valid');
    }
    else {
        $('#txtInsertProfileBranchCode').removeClass('form-control is-valid').addClass('form-control is-invalid');
    }

    if ($('#txtInsertProfileBranchCompanyCode').val() != '') {
        $('#txtInsertProfileBranchCompanyCode').removeClass('form-control is-invalid').addClass('form-control is-valid');
    }
    else {
        $('#txtInsertProfileBranchCompanyCode').removeClass('form-control is-valid').addClass('form-control is-invalid');
    }

    if ($('#txtInsertProfileBranchNameTh').val() != '') {
        $('#txtInsertProfileBranchNameTh').removeClass('form-control is-invalid').addClass('form-control is-valid');
    }
    else {
        $('#txtInsertProfileBranchNameTh').removeClass('form-control is-valid').addClass('form-control is-invalid');
    }

    $('#txtInsertProfileBranchNameEn').removeClass('form-control is-invalid').addClass('form-control is-valid');
    $('#txtInsertProfileBranchDescription').removeClass('form-control is-invalid').addClass('form-control is-valid');



}


function validateUpdateProfileBranch() {

    if ($('#txtUpdateProfileBranchCode').val() != '') {
        $('#txtUpdateProfileBranchCode').removeClass('form-control is-invalid').addClass('form-control is-valid');
    }
    else {
        $('#txtUpdateProfileBranchCode').removeClass('form-control is-valid').addClass('form-control is-invalid');
    }

    if ($('#txtUpdateProfileBranchCompanyCode').val() != '') {
        $('#txtUpdateProfileBranchCompanyCode').removeClass('form-control is-invalid').addClass('form-control is-valid');
    }
    else {
        $('#txtUpdateProfileBranchCompanyCode').removeClass('form-control is-valid').addClass('form-control is-invalid');
    }

    if ($('#txtUpdateProfileBranchNameTh').val() != '') {
        $('#txtUpdateProfileBranchNameTh').removeClass('form-control is-invalid').addClass('form-control is-valid');
    }
    else {
        $('#txtUpdateProfileBranchNameTh').removeClass('form-control is-valid').addClass('form-control is-invalid');
    }

    $('#txtUpdateProfileBranchNameEn').removeClass('form-control is-invalid').addClass('form-control is-valid');
    $('#txtUpdateProfileBranchDescription').removeClass('form-control is-invalid').addClass('form-control is-valid');



}