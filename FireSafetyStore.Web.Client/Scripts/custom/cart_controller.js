
function validatesubmit() {
        var month = $('#cc-expiration-mm').val();
        var re2digit = /^\d{2}$/;
        if (!re2digit.test(month)) {
            event.preventDefault();
            //alert('invalid expiry month format expecting a 2 digit number ranging from 1 to 12, eg: 01,02...,12 etc');
        }
        else if (month > 12 || month < 1) {
            event.preventDefault();
            //alert('invalid format expecting a 2 digit number ranging from 1 to 12');
        }



        var date = new Date();
        var currentYear = date.getFullYear();
        var futureYearAllowedRange = currentYear + 10;
        var year = $('#cc-expiration-yyyy').val();
        var re4digit = /^\d{4}$/;
        if (!re4digit.test(year)) {
            event.preventDefault();
            //alert('invalid format expecting a 3 digit number, eg: 123');
        }
        else if (year < currentYear || year > futureYearAllowedRange) {
            event.preventDefault();
            //alert('invalid expiry year format expecting a 4 digit year ranging from ' + currentYear.toString() + ' to ' + futureYearAllowedRange);
        }



        var cvv = $('#cc-cvv').val();
        var re3digit = /^\d{3}$/;
        if (!re3digit.test(cvv)) {
            event.preventDefault();
            //alert('invalid cvv format expecting a 3 digit number, eg: 123');
        }


        var name = $('#cc-name').val();
        if (name) {
            event.preventDefault();
            //alert('enter your name as in debit/credit card');
        }


        var cvv = $('#cc-number').val();
        var re16digit = /^\d{16}$/;
        if (!re16digit.test(cvv)) {
            event.preventDefault();
            //alert('enter your 16 digit debit/cedit card number');
        }

    
}

function on_expiry_month_changed() {
    var month = $('#cc-expiration-mm').val();
    var re2digit = /^\d{2}$/;
    if (!re2digit.test(month)) {
        event.preventDefault();
        alert('invalid format expecting a 2 digit number ranging from 1 to 12, eg: 01,02...,12 etc');
    }
    else if (month > 12 || month < 1) {
        event.preventDefault();
        alert('invalid format expecting a 2 digit number ranging from 1 to 12');
    }
}

function on_expiry_year_changed() {
    var date = new Date();
    var currentYear = date.getFullYear();
    var futureYearAllowedRange = currentYear + 10;
    var year = $('#cc-expiration-yyyy').val();
    var re4digit = /^\d{4}$/;
    if (!re4digit.test(year)) {
        event.preventDefault();
        alert('invalid format expecting a 3 digit number, eg: 123');
    }
    else if (year < currentYear || year > futureYearAllowedRange) {
        event.preventDefault();
        alert('invalid format expecting a 4 digit year ranging from ' + currentYear.toString() + ' to ' + futureYearAllowedRange);
    }
}

function oncvvchanged() {
    var cvv = $('#cc-cvv').val();
    var re3digit = /^\d{3}$/;
    if (!re3digit.test(cvv)) {
        event.preventDefault();
        alert('invalid format expecting a 3 digit number, eg: 123');
    }
}
//cc-name
function onccnamechanged() {
    var name = $('#cc-name').val();
    if (name) {
        event.preventDefault();
        alert('enter your name as in debit/credit card');
    }
}
function onccnumberchanged() {
    var cvv = $('#cc-number').val();
    var re16digit = /^\d{16}$/;
    if (!re16digit.test(cvv)) {
        event.preventDefault();
        alert('enter your 16 digit debit/cedit card number');
    }
}