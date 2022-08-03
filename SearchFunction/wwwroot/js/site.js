// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function GetFormattedDate(inputDate) {

    if (inputDate == "0") {

        var fmtDate = ('00');
        var fmtMonth = ('00');
        var fmtYear = ('0000');

        var FormattedStringDate = fmtMonth + '/' + fmtDate + '/' + fmtYear;

        return FormattedStringDate;
    }
    else {

        var day = parseInt(inputDate.substr(6), 10);
        var month = parseInt(inputDate.substr(4, 2), 10);
        var year = parseInt(inputDate.substr(0, 4), 10);

        var date = new Date(year, month - 1, day);

        var fmtDate = ('0' + date.getDate()).slice(-2);
        var fmtMonth = ('0' + (date.getMonth() + 1)).slice(-2);
        var fmtYear = date.getFullYear().toString()

        var FormattedStringDate = fmtMonth + '/' + fmtDate + '/' + fmtYear;

        return FormattedStringDate;
    }
}