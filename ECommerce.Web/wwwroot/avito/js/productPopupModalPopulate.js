function populateProductPopupModal(productId) {

    var productName = document.getElementById('productPopupModalName');
    var productPrice = document.getElementById('productPopupModalPrice');
    var productDescription = document.getElementById('productPopupModalDescription');
    var productImage = document.getElementById("productPopupBackgroudImage");
    var productViewLink = document.getElementById("productPopupModalLink");

    $.ajax({
        type: "GET",
        dataType: 'json',
        url: "/Shop/GetProductJson",
        traditional: true,
        data: {
            id: productId
        },
        success: function (result) {
            productName.innerText = result.name;
            productPrice.innerText = '$' + result.price;
            productDescription.innerText = result.description;
            productImage.src = result.imageUrl;
            productViewLink.href = `/Shop/ViewProduct/${result.id}`
        }
    });
}