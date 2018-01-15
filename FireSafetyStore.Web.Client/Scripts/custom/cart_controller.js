$(document).ready(function () {
    var serviceURL = '/shopping/index';

    $.ajax({
        type: "POST",
        url: serviceURL,
        data: param = "",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: successFunc,
        error: errorFunc
    });

    function successFunc(data, status) {
        alert(data);
    }

    function errorFunc() {
        alert('error');
    }
});

function goToCart() {
    alert('HI i am good');
}