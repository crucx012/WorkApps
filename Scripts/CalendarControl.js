function PositionInfo(object) {

    var pElm = object;

    this.getElementLeft = getElementLeft;
    function getElementLeft() {
        var x = 0;
        var elm;
        if (typeof (pElm) == "object") {
            elm = pElm;
        } else {
            elm = document.getElementById(pElm);
        }
        while (elm != null) {
            if (elm.style.position == 'relative') {
                break;
            }
            else {
                x += elm.offsetLeft;
                elm = elm.offsetParent;
            }
        }
        return parseInt(x);
    }

    this.getElementWidth = getElementWidth;
    function getElementWidth() {
        var elm;
        if (typeof (pElm) == "object") {
            elm = pElm;
        } else {
            elm = document.getElementById(pElm);
        }
        return parseInt(elm.offsetWidth);
    }

    this.getElementRight = getElementRight;
    function getElementRight() {
        return getElementLeft(pElm) + getElementWidth(pElm);
    }

    this.getElementTop = getElementTop;
    function getElementTop() {
        var y = 0;
        var elm;
        if (typeof (pElm) == "object") {
            elm = pElm;
        } else {
            elm = document.getElementById(pElm);
        }
        while (elm != null) {
            if (elm.style.position == 'relative') {
                break;
            }
            else {
                y += elm.offsetTop;
                elm = elm.offsetParent;
            }
        }
        return parseInt(y);
    }

    this.getElementHeight = getElementHeight;
    function getElementHeight() {
        var elm;
        if (typeof (pElm) == "object") {
            elm = pElm;
        } else {
            elm = document.getElementById(pElm);
        }
        return parseInt(elm.offsetHeight);
    }

    this.getElementBottom = getElementBottom;
    function getElementBottom() {
        return getElementTop(pElm) + getElementHeight(pElm);
    }
}

function CalendarControl() {

    var calendarId = 'CalendarControl';
    var currentYear = 0;
    var currentMonth = 0;
    var currentDay;

    var selectedYear = 0;
    var selectedMonth = 0;
    var selectedDay = 0;

    var months = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];
    var dateField = null;

    function setElementProperty(pProperty, pValue, pElmId) {
        var pElm = pElmId;
        var elm;

        if (typeof (pElm) == "object") {
            elm = pElm;
        } else {
            elm = document.getElementById(pElm);
        }
        if ((elm != null) && (elm.style != null)) {
            elm = elm.style;
            elm[pProperty] = pValue;
        }
    }

    function setProperty(pProperty, pValue) {
        setElementProperty(pProperty, pValue, calendarId);
    }

    function getDaysInMonth(year, month) {
        return [31, ((!(year % 4) && ((year % 100) || !(year % 400))) ? 29 : 28), 31, 30, 31, 30, 31, 31, 30, 31, 30, 31][month - 1];
    }

    function getDayOfWeek(year, month, day) {
        var date = new Date(year, month - 1, day);
        return date.getDay();
    }

    this.clearDate = clearDate;
    function clearDate() {
        dateField.value = '';
        hide();
    }

    this.setDate = setDate;
    function setDate(year, month, day) {
        if (dateField) {
            if (month < 10) { month = "0" + month; }
            if (day < 10) { day = "0" + day; }

            var dateString = month + "-" + day + "-" + year;
            dateField.value = dateString;
            hide();
        }
        return;
    }

    this.changeMonth = changeMonth;
    function changeMonth(change) {
        currentMonth += change;
        currentDay = 0;
        if (currentMonth > 12) {
            currentMonth = 1;
            currentYear++;
        } else if (currentMonth < 1) {
            currentMonth = 12;
            currentYear--;
        }

        var calendar = document.getElementById(calendarId);
        calendar.innerHTML = calendarDrawTable();
    }

    this.changeYear = changeYear;
    function changeYear(change) {
        currentYear += change;
        currentDay = 0;
        var calendar = document.getElementById(calendarId);
        calendar.innerHTML = calendarDrawTable();
    }

    function getCurrentYear() {
        var year = new Date().getYear();
        if (year < 1900) year += 1900;
        return year;
    }

    function getCurrentMonth() {
        return new Date().getMonth() + 1;
    }

    function getCurrentDay() {
        return new Date().getDate();
    }

    function calendarDrawTable() {

        var dayOfMonth = 1;
        var validDay = 0;
        var startDayOfWeek = getDayOfWeek(currentYear, currentMonth, dayOfMonth);
        var daysInMonth = getDaysInMonth(currentYear, currentMonth);
        var cssClass; //CSS class for each day

        var table = "<table cellspacing='0' cellpadding='0' border='0'>";
        table = table + "<tr class='header'>";
        table = table + "  <td colspan='2' class='previous'><a href='javascript:changeCalendarControlMonth(-1);'>&laquo;</a></td>";
        table = table + "  <td colspan='3' class='title'>" + months[currentMonth - 1] + "<br>" + currentYear + "</td>";
        table = table + "  <td colspan='2' class='next'><a href='javascript:changeCalendarControlMonth(1);'>&raquo;</a></td>";
        table = table + "</tr>";
        table = table + "<tr><th>S</th><th>M</th><th>T</th><th>W</th><th>T</th><th>F</th><th>S</th></tr>";

        for (var week = 0; week < 6; week++) {
            table = table + "<tr>";
            for (var dayOfWeek = 0; dayOfWeek < 7; dayOfWeek++) {
                if (week == 0 && startDayOfWeek == dayOfWeek) {
                    validDay = 1;
                } else if (validDay == 1 && dayOfMonth > daysInMonth) {
                    validDay = 0;
                }

                if (validDay) {
                    if (dayOfMonth == selectedDay && currentYear == selectedYear && currentMonth == selectedMonth) {
                        cssClass = 'current';
                    } else if (dayOfWeek == 0 || dayOfWeek == 6) {
                        cssClass = 'weekend';
                    } else {
                        cssClass = 'weekday';
                    }

                    table = table + "<td><a class='" + cssClass + "' href=\"javascript:setCalendarControlDate(" + currentYear + "," + currentMonth + "," + dayOfMonth + ")\">" + dayOfMonth + "</a></td>";
                    dayOfMonth++;
                } else {
                    table = table + "<td class='empty'>&nbsp;</td>";
                }
            }
            table = table + "</tr>";
        }

        table = table + "<tr class='header'><th colspan='7' style='padding: 3px;'><a href='javascript:clearCalendarControl();'>Clear</a><a href='javascript:hideCalendarControl();'>Close</a></td></tr>";
        table = table + "</table>";

        return table;
    }

    this.show = show;
    function show(field) {
        canHide = 0;

        // If the calendar is visible and associated with
        // this field do not do anything.
        if (dateField == field) {
            return;
        } else {
            dateField = field;
        }

        if (dateField) {
            try {
                var dateString = new String(dateField.value);
                var dateParts = dateString.split("-");

                selectedMonth = parseInt(dateParts[0], 10);
                selectedDay = parseInt(dateParts[1], 10);
                selectedYear = parseInt(dateParts[2], 10);
            } catch (e) { }
        }

        if (!(selectedYear && selectedMonth && selectedDay)) {
            selectedMonth = getCurrentMonth();
            selectedDay = getCurrentDay();
            selectedYear = getCurrentYear();
        }

        currentMonth = selectedMonth;
        currentDay = selectedDay;
        currentYear = selectedYear;

        if (document.getElementById) {

            var calendar = document.getElementById(calendarId);
            calendar.innerHTML = calendarDrawTable(currentYear, currentMonth);

            setProperty('display', 'block');

            var fieldPos = new PositionInfo(dateField);
            var calendarPos = new PositionInfo(calendarId);

            var x = fieldPos.getElementLeft();
            var y = fieldPos.getElementBottom();

            setProperty('left', x + "px");
            setProperty('top', y + "px");
        }
    }

    this.hide = hide;
    function hide() {
        if (dateField) {
            setProperty('display', 'none');
            setElementProperty('display', 'none', 'CalendarControlIFrame');
            dateField = null;
        }
    }

    this.visible = visible;
    function visible() {
        return dateField;
    }

    this.can_hide = canHide;
    var canHide;
}

var calendarControl = new CalendarControl();

function showCalendarControl(textField) {
    calendarControl.show(textField);
}

function clearCalendarControl() {
    calendarControl.clearDate();
}

function hideCalendarControl() {
    if (calendarControl.visible()) {
        calendarControl.hide();
    }
}

function setCalendarControlDate(year, month, day) {
    calendarControl.setDate(year, month, day);
}

function changeCalendarControlYear(change) {
    calendarControl.changeYear(change);
}

function changeCalendarControlMonth(change) {
    calendarControl.changeMonth(change);
}

$(document).click(function (e) {
    var d = e.target;

    while (d != null && d['id'] != 'CalendarControl' && d['id'] != 'Date') {
        d = d.parentNode;
    }

    if (d == null || (d['id'] != 'CalendarControl' && d['id'] != 'Date')) {
        hideCalendarControl();
    }
});

document.write("<iframe id='CalendarControlIFrame' src='javascript:false;' style='border: 0px; overlow: hidden;' title='Calendar Control'></iframe>");
document.write("<div id='CalendarControl'></div>");