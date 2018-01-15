function onBrandComboChanged() {
    alert('brand changed');

}

$(document).ready(function () {

    $('#btnShoppingCart').click(function () {

        $.ajax({
            url: "shopping/gotocart",
            type: "POST",
            data: '',
            cache: false,
            async: true,
            success: function (data) {
                alert(data);
            }
        });
    });
});