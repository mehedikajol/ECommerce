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
    var cookie = $.cookie('CartProducts');

    if (cookie == undefined) {
        $.cookie('CartProducts', id);
    } else {
        if (cookie.includes(id) == false) {
            $.cookie('CartProducts', $.cookie('CartProducts') + "---" + id);
        }
    }
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
    $("#cart-dropdown").empty();
    var html = "";
    var total = 0;

    getProductJsonData(function (result) {
        $.each(result, function (index, value) {
            total += value.price;
            html += '<div class="media">' +
                '<a class="pull-left" target="_blank" href="/Shop/ViewProduct/' + value.id + '">' +
                '<img class="media-object object-cover" style="height: 80px; width: 60px;" src="' + value.imageUrl + '" alt="' + value.name + '" />' +
                '</a>' +
                '<div class="media-body">' +
                '<h4 class="media-heading" style="padding-bottom: 15px;">' +
                '<a class="pull-left" target="_blank" href="/Shop/ViewProduct/' + value.id + '">' + value.name + '</a>' +
                '</h4>' +
                '<h5><strong>$' + value.price + '</strong></h5>' +
                '</div>' +
                '</div>'
        });
        $("#cart-dropdown").append($.parseHTML(html));
        $("#cart-total-price").html('$' + total + '.0');
    });
}

// GetProductsJson data
function getProductJsonData(callback) {
    var cookieValue = $.cookie('CartProducts');
    var result;
    $.ajax({
        type: "GET",
        dataType: 'json',
        url: "Cart/GetCartProductJson",
        traditional: true,
        data: {
            cookie: cookieValue
        },
        success: function (value) {
            result = value;
            callback(result);
        }
    });
}

// Update cart count
function updateCartProductCount() {
    var cookieValue = $.cookie('CartProducts');
    if (cookieValue) {
        var products = cookieValue?.split("---");
        products = products.filter((element) => element !== "");
        $('#cartProductCount').html(products.length);
    }
}

// Generate product thumbnails
function generateProductThumbnail(value) {
    return '<div class="col-md-4 remove-class">' +
        '<div class="product-item">' +
        '<div class="product-thumb">' +
        '<span class="bage">New</span>' +
        '<img class="img-responsive object-cover" src="' + value.imageUrl + '" style="height: 315px;" alt="product-img" />' +
        '<div class="preview-meta">' +
        '<ul>' +
        '<li>' +
        '<span data-toggle="modal" data-target="#product-popup-modal" onclick="populateProductPopupModal(' + `'${value.id}'` + ')">' +
        '<i class="tf-ion-ios-search-strong"></i>' +
        '</span>' +
        '</li>' +
        '<li>' +
        '<a href="#!">' +
        '<i class="tf-ion-ios-heart"></i>' +
        '</a>' +
        '</li>' +
        '<li>' +
        '<a href="#!" onclick = "addThisProductToCart(' + `'${value.id}'` + ')" >' +
        '<i class="tf-ion-android-cart"></i>' +
        '</a>' +
        '</li>' +
        '</ul>' +
        '</div>' +
        '</div>' +
        '<div class="product-content">' +
        '<h4>' +
        '<a href="/Shop/ViewProduct/' + value.id + '">' + value.name + '</a>' +
        '</h4>' +
        '<p class="price">$' + value.price + '</p>' +
        '</div>' +
        '</div>' +
        '</div>';
}

// generate pagination
function generatePagination(totalItems, currentPage, pageSize) {
    let startPage = currentPage - 3;
    let endPage = currentPage + 3;
    let totalPages = Math.ceil(totalItems / pageSize);

    if (startPage <= 0) {
        endPage = endPage - startPage + 1;
        startPage = 1;
    }

    if (endPage > totalPages) {
        endPage = totalPages;
        startPage = 1;
        if (endPage > 7) {
            startPage = endPage - 6;
        }
    }

    let result = "";
    result += '<div class="col-xs-12 text-center">' +
        '<nav aria-label="Page navigation">' +
        '<ul class="pagination">' +
        '<li onclick="updateCurrentPageNumber(1)" style="' + `${currentPage == 1 ? "pointer-events: none" : ""}` + '">' +
        '<a href="#!" aria-label="First">' +
        '<span aria-hidden="true">&#8920;</span>' +
        '</a>' +
        '</li>' +
        '<li onclick="updateCurrentPageNumber(' + (currentPage - 1) + ')" style="' + `${currentPage == 1 ? "pointer-events: none" : ""}` + '">' +
        '<a href="#!" aria-label="Previous">' +
        '<span aria-hidden="true">&#8810;</span>' +
        '</a>' +
        '</li>';

    for (let i = startPage; i <= endPage; i++) {
        result += '<li class="' + `${currentPage == i ? "active" : ""}` + '" onclick="updateCurrentPageNumber(' + i + ')">' +
            '<a href="#!">' + i + '</a>' +
            '</li>';
    }

    result += '<li onclick="updateCurrentPageNumber(' + (currentPage + 1) + ')" style="' + `${currentPage == totalPages ? "pointer-events: none" : ""}` + '">' +
        '<a href="#!" aria-label="Next">' +
        '<span aria-hidden="true">&#8811;</span>' +
        '</a>' +
        '</li>' +
        '<li onclick="updateCurrentPageNumber(' + totalPages + ')" style="' + `${currentPage == totalPages ? "pointer-events: none" : ""}` + '">' +
        '<a href="#!" aria-label="Last">' +
        '<span aria-hidden="true">&#8921;</span>' +
        '</a>' +
        '</li>' +
        '</ul>' +
        '</nav>' +
        '</div>';
    return result;
}

// current page number
function updateCurrentPageNumber(number) {
    $("#current-page-number").val(number);
    paginationValueChange();
}