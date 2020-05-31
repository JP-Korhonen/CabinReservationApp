function SelectStartingDate(dateText) {
    var date = dateText;
    var datearray = date.split(".");
    var newdate = datearray[1] + '/' + datearray[0] + '/' + datearray[2];

    // Setting date in HiddenFor-Starting
    $("#Starting").val(newdate);

    if ($("#Ending").val() < newdate && $("#Ending").val() != "") {
        SelectEndingDate(dateText);
    }

    $("#EndingDatePicker").datepicker('option', 'minDate', dateText);

}

function SelectEndingDate(dateText) {
    var date = dateText;
    var datearray = date.split(".");
    var newdate = datearray[1] + '/' + datearray[0] + '/' + datearray[2];

    // Setting date in HiddenFor-Departure
    $("#Ending").val(newdate);
}

// Creating jQuery-datepickers for selecting Starting-/EndingDates
$(function () {

    var endingMinDate = new Date(0);

    if ($("#Starting").val() != "") {
        endingMinDate = new Date($("#Starting").val())
    }

    // Setting options for datepicker
    $("#StartingDatePicker").datepicker({
        dateFormat: "dd.mm.yy"
        , firstDay: 1
        , dayNames: ["Sunnuntai", "Maanantai", "Tiistai", "Keskiviikko", "Torstai", "Perjantai", "Lauantai"]
        , dayNamesMin: ["Su", "Ma", "Ti", "Ke", "To", "Pe", "La"]
        , monthNames: ["Tammi", "Helmi", "Maalis", "Huhti", "Touko", "Kesä", "Heinä", "Elo", "Syys", "Loka", "Marras", "Joulu"]
        , nextText: "Seuraava"
        , prevText: "Edellinen"
        , onSelect: function (dateText, inst) {
            SelectStartingDate(dateText);
        }
    });

    $("#EndingDatePicker").datepicker({
        dateFormat: "dd.mm.yy"
        , minDate: endingMinDate
        , firstDay: 1
        , dayNames: ["Sunnuntai", "Maanantai", "Tiistai", "Keskiviikko", "Torstai", "Perjantai", "Lauantai"]
        , dayNamesMin: ["Su", "Ma", "Ti", "Ke", "To", "Pe", "La"]
        , monthNames: ["Tammi", "Helmi", "Maalis", "Huhti", "Touko", "Kesä", "Heinä", "Elo", "Syys", "Loka", "Marras", "Joulu"]
        , nextText: "Seuraava"
        , prevText: "Edellinen"
        , onSelect: function (dateText, inst) {
            SelectEndingDate(dateText);
        }
    });
});