function showAllert(id) {
    if ($.cookie('product') == undefined) {
        $.cookie('product', id);
    } else {
        $.cookie('product', $.cookie('product') + "---" + id);
    }
    var myCookie = $.cookie('product');
    window.alert(myCookie);
}