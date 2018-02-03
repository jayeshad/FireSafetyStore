
function onpaymentinfochanged() {
    var number = $('#txtcardnumber').val();
    var name = $('#txtnameincard').val();
    var cvv = $('#txtcvvcode').val();
    if (number && name && cvv) {
        document.getElementById("btnpayment").disabled = false;        
    }
    
    else {
        document.getElementById("btnpayment").disabled = true;
        alert('debit/credit card info is mandatory to proceed with payment');
    }
    
}

function validatePaymentInfo() {
    debugger;
    var creditcardno = $('#txtcardnumber').val();
    var re16digit = /^\d{16}$/;
    if (!re16digit.test(creditcardno)) {
        alert("Please enter your 16 digit credit card numbers");
        return false;
    }
}

$(function () {
    $('#txtcardnumber').validateCreditCard(function (result) {
        $('#logerror').html('Card type: ' + (result.card_type == null ? '-' : result.card_type.name)
                 + '<br>Valid: ' + result.valid
                 + '<br>Length valid: ' + result.length_valid
                 + '<br>Luhn valid: ' + result.luhn_valid);
    });
});
    