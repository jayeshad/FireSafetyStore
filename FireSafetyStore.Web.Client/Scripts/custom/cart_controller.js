
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
    