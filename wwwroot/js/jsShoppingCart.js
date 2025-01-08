$(document).ready(function () {
    ShowCount();    
    $('body').on('click', '.btnAddToCart', function (e) {
        e.preventDefault();
        var id = $(this).data('id');
        var quantity = 1;
        var tQuantity = $('quantity_value').text();
        if (tQuantity != '') {
            quantity = parseInt(tQuantity);
        }

        alert(id + " " + quantity);
        $.ajax({
            url: '/shoppingcart/AddToCart',
            type: 'POST',
            date: { id: id, quantity: quantity },
            success: function (rs) {
                if (rs.success) {
                    $('#cartItemCount').html(rs.Count);
                    alert(rs.massage);
                }
            }
        });
    });
    $('body').on('click', '.btnDeleteAll', function (e) {
        e.preventDefault();
        var id = $(this).data('id');
        var conf = confirm('Bạn có chắc muốn xóa hết sản phẩm trong giỏ hàng?');
        if (conf == true) {
            DeleteAll();
        }``
    });

    $('body').on('click', '.btnDelete', function (e) {
        e.preventDefault();
        var id = $(this).data('id');
        var conf = confirm('Bạn có chắc chắn xóa sản phẩm này khỏi giỏ hàng?');
        if (conf == true) {
            $.ajax({
                url: '/shoppingcart/Delete',
                type: 'POST',
                date: { id: id },
                success: function (rs) {
                    if (rs.success) {
                        $('#cartItemCount').html(rs.Count);
                        $('#trow_' + id).remove();
                    }
                }
            });
        }
    });

});

function ShowCount() {
    $.ajax({
        url: '/shoppingcart/AddToCart',
        type: 'GET',
        success: function (rs) {
            $('#cartItemCount').html(rs.Count);
        }
    });
}
function DeleteAll() {
    $.ajax({
        url: '/shoppingcart/DeleteAll',
        type: 'POST',
        success: function (rs) {
            if (rs.success) {
                LoadCart();
            }
        }
    });
}

function LoadCart() {
    $.ajax({
        url: '/shoppingcart/Partial_Item_Cart',
        type: 'GET',
        success: function (rs) {
            $('#load_data').html(rs);
        }
    });
}