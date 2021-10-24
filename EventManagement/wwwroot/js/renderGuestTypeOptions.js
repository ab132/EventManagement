$(document).ready(function () {
    $('#LegalPersonDivId').hide();

    $('#PrivateGuestTypeSelectId').change(function () {
        var value = $(this).val();

        if (value == "LegalPerson") {

            $('#PrivateGuestDivId').hide();
            $('#LegalPersonDivId').fadeIn(1000, "swing");
        }
    });
    $('#LegalPersonTypeSelectId').change(function () {
        var value = $(this).val();

        if (value == "PrivateGuest") {

            $('#LegalPersonDivId').hide();
            $('#PrivateGuestDivId').fadeIn(1000, "swing");
        }
    });
});