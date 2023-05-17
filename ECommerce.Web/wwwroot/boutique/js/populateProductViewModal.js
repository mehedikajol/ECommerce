function populateProductPopupModal(id) {

    var productName = document.getElementById('productViewModalName');
    var productPrice = document.getElementById('productViewModalPrice');
    var productDescription = document.getElementById('productViewModalDescription');
    var productId = document.getElementById('productPopupModalId');

    // productViewModalPrice //productViewModalDescription

    $.ajax({
        type: "GET",
        dataType: 'json',
        url: "/Shop/GetProductJson",
        traditional: true,
        data: {
            id: id
        },
        success: function (result) {
            console.log(result);
            productName.innerText = result.name;
            productPrice.innerText = '$' + result.id;
            productDescription.innerText = result.description;
            //productPrice.innerHTML = result.Id;
            document.getElementById("productModalViewBackgroudImage").style.backgroundImage = 'url("' + result.imageUrl + '")';
        }
    });

    
    //console.log(productNameTag);
}