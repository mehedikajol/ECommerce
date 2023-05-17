function populateProductPopupModal(id) {

    var productName = document.getElementById('productPopupModalName');
    var productPrice = document.getElementById('productPopupModalPrice');
    var productDescription = document.getElementById('productPopupModalDescription');
    var productImage = document.getElementById("productPopupBackgroudImage");
    var productViewLink = document.getElementById("productPopupModalLink");
    var productButton = document.getElementById("productPopupModalButton");
    // productPopupModalButton

    $.ajax({
        type: "GET",
        dataType: 'json',
        url: "/Shop/GetProductJson",
        traditional: true,
        data: {
            id: id
        },
        success: function (result) {
            productName.innerText = result.name;
            productPrice.innerText = '$' + result.price;
            productDescription.innerText = result.description;
            productImage.src = result.imageUrl;
            productViewLink.href = `/Shop/ViewProduct/${result.id}`
        }
    });

    $(productButton).click(function () {
        addThisProductToCart(`${id}`);
    });
}