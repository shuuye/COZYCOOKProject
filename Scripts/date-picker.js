document.addEventListener("DOMContentLoaded", function (event) {


    AddDatePickers();


    // Listen for changes in the date picker value
    document.getElementById('dateip').addEventListener('change', handleDatePickerChange);

    // Initialize time options based on the initial date picker value
    handleDatePickerChange();

    // Clear date input field when clicked to prevent history showing
    document.getElementById('dateip').addEventListener('click', function () {
        this.value = ''; // Clear the value
        document.getElementById('timeip').value = '';
    });

    // Clear date input field when clicked to prevent history showing
    document.getElementById('timeip').addEventListener('click', function () {
        this.value = ''; // Clear the value
    });
});

function AddDatePickers() {

    window.Months = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];
    window.MonthsShort = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
    var DatePickers = document.getElementsByClassName('date-picker');

    for (i = 0; i < DatePickers.length; i++) {

        DatePickers[i].addEventListener('click', function (event) {

            AddCalendarDays((new Date()).getFullYear(), (new Date()).getMonth(), event.currentTarget);

        })

    }

}
function AddCalendarDays(Year, Month, Object) {
    if (Object) { window.DateObject = Object }

    if (!window.DatePickerHolder) {
        window.DatePickerHolder = document.createElement('div');
        window.DatePickerHolder.className = "date-picker-holder";
        window.DatePickerTable = document.createElement('table');
        window.DatePickerTable.className = "date-picker-table";
        document.body.appendChild(window.DatePickerHolder);
        document.body.appendChild(window.DatePickerTable);
    } else {
        window.DatePickerHolder.style.display = 'block';
        window.DatePickerTable.style.display = 'table';
        window.DatePickerTable.innerHTML = '';
    }

    var today = new Date();
    var currentMonth = today.getMonth();
    var currentYear = today.getFullYear();

    if (Month < 0) {
        Year = Year - 1;
        Month = 11;
    }

    if (Month > 11) {
        Year = Year + 1;
        Month = 0;
    }

    window.Month = Month;
    window.Year = Year;

    var firstDay = new Date(window.Year, window.Month, 1);
    var lastDay = new Date(window.Year, window.Month + 1, 0);

    var DayOffset = firstDay.getDay();
    var Day = 1;
    var MaxDay = lastDay.getDate();
    var Done = false;

    var HeaderRow = "<table><tr><td onclick='AddCalendarDays(window.Year, window.Month - 1);'>❮</td><td onclick='AddCalendarMonths(window.Year);'>" + window.Months[window.Month] + " " + window.Year.toString() + "</td><td onclick='AddCalendarDays(window.Year, window.Month + 1);' >❯</td><td><button onclick='CloseCalendar()'>Close</button></td></tr></table>";

    var MainContent = "<table><tr><td>Su</td><td>Mo</td><td>Tu</td><td>We</td><td>Th</td><td>Fr</td><td>Sa</td></tr>";

    for (r = 0; r < 6; r++) {
        if (!Done) {
            MainContent += "<tr>";
            for (c = 0; c < 7; c++) {
                if (DayOffset > 0) {
                    DayOffset = DayOffset - 1;
                } else {
                    var currentDate = new Date(window.Year, window.Month, Day);
                    if (Day <= MaxDay) {
                        var disabledClass = '';
                        if (currentDate < today || (currentYear == Year && currentDate.getMonth() < currentMonth) || (currentYear == Year && currentDate.getMonth() > currentMonth + 2) || (Year < currentYear) || (Year == currentYear && Month < currentMonth) || (Year == currentYear && Month > currentMonth + 2)) {
                            disabledClass = ' disabled-date';
                        }
                        MainContent += "<td class='" + disabledClass + "' onclick='SetDate(this);'>" + "<span style='color: rgb(88, 86, 86);'>" + Day + "</span>" + "</td>";
                        Day = Day + 1;
                    } else {
                        Done = true;
                    }
                }
            }
            MainContent += "</tr>";
        }
    }
    MainContent += "</table>";

    window.DatePickerTable.innerHTML = "<tr><td>" + HeaderRow + "</td></tr><tr><td>" + MainContent + "</td></tr><tr><td><button onclick='ClearDate()'>Clear</button></td></tr>";
}

function CloseCalendar() {
    if (window.DatePickerHolder && window.DatePickerTable) {
        document.body.removeChild(window.DatePickerHolder);
        document.body.removeChild(window.DatePickerTable);
        window.DatePickerHolder = null;
        window.DatePickerTable = null;
    }
}

function ClearDate() {
    if (window.DateObject) {
        window.DateObject.value = "";
        CloseCalendar();
        document.getElementById('timeip').value = '';
        document.getElementById('time-slot').disabled = true;
    }
}



function AddCalendarMonths(Year) {

    window.DatePickerTable.innerHTML = '';

    window.Year = Year;

    var Done = false;

    var HeaderRow = "<table><tr><td onclick='AddCalendarMonths(window.Year - 1);'><</td><td>" + window.Year.toString() + "</td><td onclick='AddCalendarMonths(window.Year + 1);' >></td></tr></table>";

    var MainContent = "<table>";

    for (r = 0; r < 3; r++) {
        MainContent += "<tr>";
        for (c = 0; c < 4; c++) {
            var MonthNo = (r * 4) + c;
            MainContent += "<td onclick='AddCalendarDays(window.Year, " + MonthNo + ", null);'>";
            MainContent += window.Months[MonthNo].substring(0, 3);
            MainContent += "</td>";
        }
        MainContent += "</tr>";
    }

    MainContent += "</table>";

    window.DatePickerTable.innerHTML = "<tr><td>" + HeaderRow + "</td></tr><tr><td>" + MainContent + "</td></tr>";

}

function SetDate(Obj) {
    if (Obj.textContent != '') {
        window.DateObject.value = Obj.textContent.trim() + ' ' + window.Months[window.Month] + ' ' + window.Year.toString();
        CloseCalendar();
        handleDatePickerChange();
    }
}

function ClearDatePicker() {
    if (window.DatePickerHolder) {
        window.DatePickerHolder.style.display = 'none';
    }
    if (window.DatePickerTable) {
        window.DatePickerTable.style.display = 'none';
    }
    window.Month = null;
    window.Year = null;
}

function FormatDate(DateString) {
    var D = new Date(DateString);
    return D.getDate().toString() + " " + window.Months[D.getMonth()] + " " + D.getFullYear().toString();
}

function FormatDateShort(DateString) {
    var D = new Date(DateString);
    return D.getDate().toString() + " " + window.MonthsShort[D.getMonth()] + " " + D.getFullYear().toString();
}


/*for time picker*/
// Function to add options to the select element
function addOptionsToSelect(startHour, endHour) {
    const selectElement = document.getElementById('time-slot');
    const foption = document.createElement('option');
    foption.value = 0;
    foption.textContent = "--";
    selectElement.appendChild(foption);
    selectElement.innerHTML = ''; // Clear existing options
    for (let hour = startHour; hour <= endHour; hour++) {
        for (let minute = 0; minute < 60; minute += 60) { // Adding options for every hour
            const time = `${String(hour).padStart(2, '0')}:${String(minute).padStart(2, '0')}`;
            const option = document.createElement('option');
            option.value = time;
            option.textContent = formatTime(time);
            selectElement.appendChild(option);
        }
    }

    // Add onchange event listener to set the selected time to the input field
    selectElement.addEventListener('change', SetTime);
}

// Format time string (e.g., '07:00' => '7:00 AM')
function formatTime(timeString) {
    const [hours, minutes] = timeString.split(':');
    const hour = parseInt(hours, 10);
    const suffix = hour >= 12 ? 'PM' : 'AM';
    const formattedHour = hour % 12 === 0 ? 12 : hour % 12;
    return `${formattedHour}:${minutes} ${suffix}`;
}

// Function to handle date picker change
function handleDatePickerChange() {
    var selectedDate = document.getElementById('dateip').value;
    const selectElement = document.getElementById('time-slot');
    const dayOfWeek = selectedDate ? (new Date(selectedDate)).getDay() : -1;

    // Define operation hours based on the selected date
    let startHour, endHour;
    if (dayOfWeek >= 1 && dayOfWeek <= 5) {
        // Weekday (Monday to Friday)
        startHour = 7; // Start from 7 AM
        endHour = 22; // End at 10 PM
    } else {
        // Weekend (Saturday and Sunday)
        startHour = 9; // Start from 9 AM
        endHour = 22; // End at 10 PM
    }

    // Add options for each hour between start and end hours
    addOptionsToSelect(startHour, endHour);

    // Enable or disable the time slot dropdown based on whether a date is selected
    selectElement.disabled = !selectedDate;
}

function SetTime() {
    const selectedTime = document.getElementById('time-slot').value;
    document.getElementById('timeip').value = selectedTime; // Set the selected time to the input field
    var popupConfirm = event.currentTarget.closest('.popupConfirm');
    popupConfirm.classList.remove("show");
}

