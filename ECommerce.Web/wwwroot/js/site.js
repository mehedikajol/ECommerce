// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
window.onload = function () {
    updateCartProductCount();
};

window.addEventListener("focus", function () {
    updateCartProductCount();
});

// Product add to cart
function addThisProductToCart(id) {
    if ($.cookie('CartProducts') == undefined) {
        $.cookie('CartProducts', id);
    } else {
        $.cookie('CartProducts', $.cookie('CartProducts') + "---" + id);
    }
    var myCookie = $.cookie('CartProducts');
    Swal.fire({
        title: '<strong>Succes</strong>',
        icon: 'success',
        html: '<strong>Product added to cart.</strong>',
    });
    updateCartProductCount();
}

// Remove a product from cart
function removeThisProductFromCart(id) {
    var cookieValue = $.cookie('CartProducts');
    var newValue = cookieValue.replace(id, '');
    $.cookie('CartProducts', newValue);
    updateCartProductCount();
}

// Cart icon hover 
function populateCartDropDown() {
    var cookieValue = $.cookie('CartProducts');
    $.ajax({
        type: "GET",
        dataType: 'json',
        url: "Cart/GetCartProductJson",
        traditional: true,
        data: {
            cookie: cookieValue
        },
        success: function (result) {
            $("#cart-dropdown").empty();
            var html = "";
            var total = 0;
            $.each(result, function (index, value) {
                total += value.price;
                html += '<div class="media">' +
                    '<a class="pull-left" target="_blank" href="/Shop/ViewProduct/' + value.id + '">' +
                    '<img class="media-object object-cover" style="height: 80px; width: 60px;" src="' + value.imageUrl + '" alt="image" />' +
                    '</a>' +
                    '<div class="media-body">' +
                    '<h4 class="media-heading">' + value.name +
                    '</h4>' +
                    '<h5>' +
                    '<strong>$' + value.price + '</strong>' +
                    '</h5>' +
                    '<button class="btn btn-sm btn-danger pull-right" style="margin-right: 5px;">Remove</button>' +
                    '</div>' +
                    '</div>'
            });
            $("#cart-dropdown").append($.parseHTML(html));
            $("#cart-total-price").html('$' + total + '.0');
        }
    });
}

// Update cart count
function updateCartProductCount() {
    var cookieValue = $.cookie('CartProducts');
    var products = cookieValue.split("---");
    products = products.filter((element) => element !== "");
    $('#cartProductCount').html(products.length);
}
