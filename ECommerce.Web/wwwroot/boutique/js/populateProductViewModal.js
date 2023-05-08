function updateModalValues(productId) {

    var productName = document.getElementById('productViewModalName');
    var productPrice = document.getElementById('productViewModalPrice');
    var productDescription = document.getElementById('productViewModalDescription');

    // productViewModalPrice //productViewModalDescription

    $.ajax({
        type: "GET",
        dataType: 'json',
        url: "/Products/GetProductById",
        traditional: true,
        data: {
            id: productId
        },
        success: function (result) {
            console.log(result.imageUrl);
            productName.innerText = result.name;
            productPrice.innerText = '$' + result.price;
            productDescription.innerText = result.description;
            document.getElementById("productModalViewBackgroudImage").style.backgroundImage = 'url("' + result.imageUrl + '")';
        }
    });

    
    //console.log(productNameTag);
}