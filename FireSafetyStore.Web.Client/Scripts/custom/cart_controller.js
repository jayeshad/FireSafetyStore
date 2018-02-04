﻿function validatecreditcard() {

    var monthisvalid = false;
    var month = $('#cc-expiration-mm').val();
    var re2digit = /^\d{2}$/;
    if (re2digit.test(month) && (month >=1 && month <= 12)) {
        monthisvalid = true;
        $('#cc-expiration-mm').removeClass('ng-invalid');
    }
    else 
        $('#cc-expiration-mm').addClass('ng-invalid');


    var yearisvalid = false;
    var date = new Date();
    var currentYear = date.getFullYear();
    var futureYearAllowedRange = currentYear + 10;
    var year = $('#cc-expiration-yyyy').val();
    var re4digit = /^\d{4}$/;
    if (re4digit.test(year) && (year >= currentYear && year <= futureYearAllowedRange)) {
        $('#cc-expiration-yyyy').removeClass('ng-invalid');
        yearisvalid = true;
    }
    else
        $('#cc-expiration-yyyy').addClass('ng-invalid');


    var cvvisvalid = false;
    var cvv = $('#cc-cvv').val();
    var re3digit = /^\d{3}$/;
    if (re3digit.test(cvv)) {
        $('#cc-cvv').removeClass('ng-invalid');
        cvvisvalid = true;
    }
    else
        $('#cc-cvv').addClass('ng-invalid');


    var nameisvalid = false;
    var name = $('#cc-name').val();
    var alpabets = /^[A-z]+$/;
    if (name && alpabets.test(name)) {
        $('#cc-name').removeClass('ng-invalid');
        nameisvalid = true;
    }
    else
        $('#cc-name').addClass('ng-invalid');


    var ccnumberisvalid = false;
    var cvv = $('#cc-number').val();
    var re16digit = /^\d{16}$/;
    if (re16digit.test(cvv)) {
        $('#cc-number').removeClass('ng-invalid');
        ccnumberisvalid = true;
    }
    else
        $('#cc-number').addClass('ng-invalid');

    if (monthisvalid && yearisvalid && cvvisvalid && nameisvalid && ccnumberisvalid)
        document.getElementById("btn-proccedpay").disabled = false;
    else
        document.getElementById("btn-proccedpay").disabled = true;

}