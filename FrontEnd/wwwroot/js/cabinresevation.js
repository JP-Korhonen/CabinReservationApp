// Requires:
//var cabinPricePerDay
//var reservedDays
//var reservedStartDays
//var reservedEndDays
//var reservedSameDays

// Submit form
function CheckFormValues() {
    // Checking that user has select all required information
    if (Duration == 0 || StartDate == null) {
        return;
    }
    // Setting Start- and EndDate in CabinReservation
    document.getElementById("ReservationStartDate").value = StartDate.toJSON();
    document.getElementById("ReservationEndDate").value = EndDate.toJSON();
    // Setting ActivityReservations in CabinReservation
    //var counter = 0;

    //for (item of ActivityIdList) {
    //    if (counter < 9)
    //        document.getElementById("ActivityReservations-" + counter).value = item;
    //    counter++;
    //}

    for (var i = 0; i < 9; i++) {
        if (ActivityIdList[i] == null) {
            document.getElementById("ActivityReservations-" + i).value = 0;
        }
        else {
            document.getElementById("ActivityReservations-" + i).value = ActivityIdList[i];
        }
    }



    // Submits Form
    $("#submitForm").click();
}

var ActivityIdList = new Array();
var ActivityListIdGenerator = 1;
var Duration = 0;
var CabinTotalPrice = 0;
var ActivitiesTotalPrice = 0;
var TotalPrice = 0;
var StartDate = new Date();
var EndDate = new Date();

// Adds ActivityId in ActivityIdList
// Calculates ActivitiesTotalPrice and TotalPrice
// Adds <li>-element in <ul id="#SelectedActivitys">-element
// Sets text in <div id="#ActivitesTotalPrice">-element
// Sets text in <div id="#TotalPrice">-element
function AddActivity(ActivityId, ActivityName, ActivityPrice) {
    if (ActivityIdList.length == 9) {
        $("#activityListError").css({ opacity: 1 });
        return;
    }
    ActivityIdList.push(ActivityId);

    ActivitiesTotalPrice = Number(ActivitiesTotalPrice) + Number(ActivityPrice);
    TotalPrice = Number(ActivitiesTotalPrice) + Number(CabinTotalPrice);

    $("#SelectedActivitys").append('<li class="list-group-item list-group-item-success" id="Activity-' + ActivityListIdGenerator +
        '" style="font-size:15px; font-weight: bold;"><button class="btn btn-danger btn-number" onclick="DeleteActivity(' + ActivityListIdGenerator + ',' + ActivityPrice + ',' + ActivityId + ')">-</button>&emsp;' +
        ActivityName + " " + ActivityPrice + " € " + '</li>');

    $("#ActivitesTotalPrice").text("Lisäpalveluiden kokonaishinta = " + ActivitiesTotalPrice + " €");
    $("#TotalPrice").text("Kokonaishinta yhteensä = " + TotalPrice + " €");

    ActivityListIdGenerator++;
}

// Removes ActivityId in ActivityIdList
// Calculates ActivitiesTotalPrice and TotalPrice
// Removes <li>-element to <ul id="#SelectedActivitys">-element by <li>-element-id
// Sets text in <div id="#ActivitesTotalPrice">-element
// Sets text in <div id="#TotalPrice">-element
function DeleteActivity(Id, ActivityPrice, ActivityId) {
    var indexToRemove = ActivityIdList.indexOf('' + ActivityId + '');
    ActivityIdList.splice(indexToRemove, 1);

    ActivitiesTotalPrice = Number(ActivitiesTotalPrice) - Number(ActivityPrice);
    TotalPrice = Number(ActivitiesTotalPrice) + Number(CabinTotalPrice);

    $("#Activity-" + Id).remove();
    $("#activityListError").css({ opacity: 0 });

    $("#ActivitesTotalPrice").text("Lisäpalveluiden kokonaishinta = " + ActivitiesTotalPrice + " €");
    $("#TotalPrice").text("Kokonaishinta yhteensä = " + TotalPrice + " €");
}

// Calculates CabinTotalPrice and TotalPrice
// Sets text in <dropdown id="#Days">-element
// Sets text in <div id="#CabinTotalPrice">-element
// Sets text in <div id="#TotalPrice">-element
function ChangeDays(days) {
    Duration = Number(days);
    if (Duration == 0) $("#WarningDurationNotSelected").css({ opacity: 1 });
    else $("#WarningDurationNotSelected").css({ opacity: 0 });

    CabinTotalPrice = Number(cabinPricePerDay) * Number(Duration);
    TotalPrice = Number(CabinTotalPrice) + Number(ActivitiesTotalPrice);

    $("#Days").text("Valitse vuorokaudet : " + Duration + " vrk");
    $("#CabinTotalPrice").text("Majoituksen kokonaishinta " + Duration + " vuorokautta = " + CabinTotalPrice + " €");
    $("#TotalPrice").text("Kokonaishinta yhteensä = " + TotalPrice + " €");

    if (StartDate != null) {
        SelectReservationDays(StartDate);
    }
}

// When user clicks calendars date
function SelectReservationDays(dateText) {
    var SelectedDatesAvaible = true;

    // Users picked dates, based on selected startdate and duration
    var UsersSelectedDates = new Array();
    StartDate = new Date(dateText);

    DateCounter = new Date(StartDate);
    for (var counter = 0; counter < Number(Duration); counter++) {
        UsersSelectedDates.push(new Date(DateCounter));
        DateCounter.setDate(DateCounter.getDate() + 1);
    }

    // Unavaible dates
    var UnavaibleDates = new Array();
    for (item of reservedDays) {
        UnavaibleDates.push(new Date(Date.parse(item)));
        if (UsersSelectedDates.find(d => d.getTime() === new Date(Date.parse(item)).getTime())) {
            SelectedDatesAvaible = false;
            StartDate = null;
            EndDate = null;
        }
    }

    var dateNow = new Date();
    // If users selected dates are avaible and duration is not null, paint selected dates darkgreen and set EndDate
    if (SelectedDatesAvaible && Duration != 0 && StartDate) {
        EndDate = new Date(StartDate.getTime() + (Duration * 86400000));
        $("#WarningDaysNotSelected").css({ opacity: 0 });

        $("#makeCabinReservationsCalendar").datepicker("option", "beforeShowDay",
            function (date) {
                if (date.getTime() < (dateNow.getTime() - 86400000)) return [true, 'redday', ""];
                if (UsersSelectedDates.find(d => d.getTime() === date.getTime())) return [true, 'darkgreenday', ""];

                if (UsersSelectedDates.find(d => d.getTime() + 86400000 === date.getTime())) return [true, 'darkgreenday', ""];

                if (sameDatesArray.find(d => d.getTime() === date.getTime())) return [true, 'redday', "Varattu"];
                if (startDatesArray.find(d => d.getTime() === date.getTime())) return [true, 'startday', ""];
                if (endDatesArray.find(d => d.getTime() === date.getTime())) return [true, 'endday', ""];
                if (UnavaibleDates.find(d => d.getTime() === date.getTime())) return [true, 'redday', "Varattu"];
                return [true, 'greenday', ''];
            });
    }

    else {
        $("#WarningDaysNotSelected").css({ opacity: 1 });
        $("#makeCabinReservationsCalendar").datepicker("option", "beforeShowDay",
            function (date) {
                if (date.getTime() < (dateNow.getTime() - 86400000)) return [true, 'redday', ""];
                if (sameDatesArray.find(d => d.getTime() === date.getTime())) return [true, 'redday', "Varattu"];
                if (startDatesArray.find(d => d.getTime() === date.getTime())) return [true, 'startday', ""];
                if (endDatesArray.find(d => d.getTime() === date.getTime())) return [true, 'endday', ""];
                if (UnavaibleDates.find(d => d.getTime() === date.getTime())) return [true, 'redday', "Varattu"];
                return [true, 'greenday', ''];
            });
    }
}

var startDatesArray = new Array();
var endDatesArray = new Array();
var sameDatesArray = new Array();

$(function () {
    // Setting StartDate to 1/1/1970 when user navigates this view
    StartDate = null;

    $("#activityListError").css({ opacity: 0 });

    var reservedDates = new Array();
    for (item of reservedDays) {
        reservedDates.push(new Date(Date.parse(item)));
    }

    for (item of reservedStartDays) {
        startDatesArray.push(new Date(Date.parse(item)));
    }

    for (item of reservedEndDays) {
        endDatesArray.push(new Date(Date.parse(item)));
    }

    for (item of reservedSameDays) {
        sameDatesArray.push(new Date(Date.parse(item)));
    }

    // https://www.dotnetcurry.com/jquery/1209/jqueryui-datepicker-tips-tricks
    // https://api.jqueryui.com/datepicker/
    var dateNow = new Date();
    $("#makeCabinReservationsCalendar").datepicker({
        minDate: 0,
        beforeShowDay: function (date) {
            if (date.getTime() < (dateNow.getTime() - 86400000)) return [true, 'redday', ""];
            if (sameDatesArray.find(d => d.getTime() === date.getTime())) return [true, 'redday', "Varattu"];
            if (startDatesArray.find(d => d.getTime() === date.getTime())) return [true, 'startday', ""];
            if (endDatesArray.find(d => d.getTime() === date.getTime())) return [true, 'endday', ""];
            if (reservedDates.find(d => d.getTime() === date.getTime())) return [true, 'redday', "Varattu"];
            return [true, 'greenday', "Vapaa"];
        }
        , firstDay: 1
        , dayNames: ["Sunnuntai", "Maanantai", "Tiistai", "Keskiviikko", "Torstai", "Perjantai", "Lauantai"]
        , dayNamesMin: ["Su", "Ma", "Ti", "Ke", "To", "Pe", "La"]
        , monthNames: ["Tammi", "Helmi", "Maalis", "Huhti", "Touko", "Kesä", "Heinä", "Elo", "Syys", "Loka", "Marras", "Joulu"]
        , nextText: "Seuraava"
        , prevText: "Edellinen"
        , onSelect: function (dateText, inst) {
            SelectReservationDays(dateText);
        }
    });

});