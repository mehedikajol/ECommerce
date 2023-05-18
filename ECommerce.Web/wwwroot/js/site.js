// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

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
}

// Remove a product from cart
function removeThisProductFromCart(id) {
    var cookieValue = $.cookie('CartProducts');
    var newValue = cookieValue.replace(id, '');
    $.cookie('CartProducts', newValue);
}