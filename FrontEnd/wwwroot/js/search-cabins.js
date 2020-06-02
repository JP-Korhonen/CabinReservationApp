// TODO: Maybe later here we can add that Departure is automatic always at least 1day greater than Arrival

function SelectArrivalDate(dateText) {
    var date = dateText;
    var datearray = date.split(".");
    var newdate = datearray[1] + '/' + datearray[0] + '/' + datearray[2];

    // Setting date in HiddenFor-Arrival
    $("#Arrival").val(newdate);

    if ($("#Departure").val() < newdate && $("#Departure").val() != "") {
        SelectDepartureDate(dateText);
    }

    $("#DepartureDatePicker").datepicker('option', 'minDate', dateText);

}

function SelectDepartureDate(dateText) {

    var date = dateText;
    var datearray = date.split(".");
    var newdate = datearray[1] + '/' + datearray[0] + '/' + datearray[2];

    // Setting date in HiddenFor-Departure
    $("#Departure").val(newdate);
}

// Creating jQuery-datepickers for selecting Arrival/DepartureDates
$(function () {

    var departureMinDate = 0;
    if ($("#Arrival").val() != "") {
        departureMinDate = new Date($("#Arrival").val())
    }

    // Setting options for datepicker
    $("#ArricalDatePicker").datepicker({
        dateFormat: "dd.mm.yy"
        , minDate: 0
        , firstDay: 1
        , dayNames: ["Sunnuntai", "Maanantai", "Tiistai", "Keskiviikko", "Torstai", "Perjantai", "Lauantai"]
        , dayNamesMin: ["Su", "Ma", "Ti", "Ke", "To", "Pe", "La"]
        , monthNames: ["Tammi", "Helmi", "Maalis", "Huhti", "Touko", "Kesä", "Heinä", "Elo", "Syys", "Loka", "Marras", "Joulu"]
        , nextText: "Seuraava"
        , prevText: "Edellinen"
        , onSelect: function (dateText, inst) {
            SelectArrivalDate(dateText);
        }
    });

    $("#DepartureDatePicker").datepicker({
        dateFormat: "dd.mm.yy"
        , minDate: departureMinDate
        , firstDay: 1
        , dayNames: ["Sunnuntai", "Maanantai", "Tiistai", "Keskiviikko", "Torstai", "Perjantai", "Lauantai"]
        , dayNamesMin: ["Su", "Ma", "Ti", "Ke", "To", "Pe", "La"]
        , monthNames: ["Tammi", "Helmi", "Maalis", "Huhti", "Touko", "Kesä", "Heinä", "Elo", "Syys", "Loka", "Marras", "Joulu"]
        , nextText: "Seuraava"
        , prevText: "Edellinen"
        , onSelect: function (dateText, inst) {
            SelectDepartureDate(dateText);
        }
    });
});