<%@ page import="model.User" %>
<%@ page import="java.util.ArrayList" %>
<%@ page import="model.Cart" %>
<%@ page import="java.util.List" %>
<%@ page import="dao.userDAO.ProductDao" %>
<%@ page import="connector.DAOConnection" %>
<%@ page import="java.text.NumberFormat" %>
<%@ page import="java.util.Locale" %>
<%--
  Created by IntelliJ IDEA.
  User: ngoke
  Date: 4/1/2024
  Time: 5:28 PM
  To change this template use File | Settings | File Templates.
--%>
<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<% NumberFormat format = NumberFormat.getInstance(new Locale("vn", "VN"));
    User auth = (User) request.getSession().getAttribute("user");
    if (auth != null) {
        request.setAttribute("user", auth);
    }
    ArrayList<Cart> cart_list = (ArrayList<Cart>) request.getSession().getAttribute("cart-list");
    List<Cart> cartProduct = null;
    double total = 0;
    if (cart_list != null) {
        ProductDao dao = new ProductDao(DAOConnection.getConnection());
        cartProduct = dao.getCartProduct(cart_list);
         total = dao.getTotalCartPrice(cart_list);
        request.setAttribute("cart_list", cart_list);
        request.setAttribute("total", total);
    }
%>
<html>
<head>
    <title>Giỏ hàng</title>
    <style type="text/css">
        .table th, .table td {
            text-align: center;
            vertical-align: middle;
        }

        .form-group {
            display: flex;
            justify-content: space-between;
            width: 20%;
        }

        .form-control {
            width: 20%;
            text-align: center;
        }

        .btn-inc, .btn-decree {
            box-shadow: none;
            font-size: 25px;
        }
    </style>
</head>
<body>
<jsp:include page="/views/header.jsp"/>
<div class="container">
    <div class="d-flex py-3">
        <h3>Tổng tiền: <span id="totalPrice"><%= cart_list != null ? format.format(total) + "đ" : 0 + "đ" %></span></h3>
        <form method="post" action="<%=request.getContextPath()%>/createOrderFromCart">
            <button type="submit" class="mx-3 btn btn-primary" href="#">Thanh toán</button>
        </form>

    </div>
    <table class="table table-light">
        <thead>
        <tr>
            <th scope="col">Sản phẩm</th>
            <th scope="col">Giá</th>
            <th scope="col">Số lượng</th>
            <th scope="col">Xóa</th>
        </tr>
        </thead>
        <tbody>

        <%
            if (cart_list != null) {
                for (Cart c : cartProduct) {
        %>
        <tr>
            <td>
                <div class="d-flex align-items-center">
                    <img src="<%=c.getImg_main()%>" alt="KKK" class="img-thumbnail"
                         style="width: 50px; height: 50px; margin-right: 10px;">
                    <span><%=c.getName()%></span>
                </div>
            </td>
            <td><%= format.format(c.getPrice()) %>đ
            </td>
            <td>
                <form action="" method="post" class="form-inline d-flex justify-content-center">
                    <input type="hidden" name="id" value="<%=c.getId()%>"
                           class="form-input">
                    <div class="form-group d-flex justify-content-between w-50">
                        <a class="btn btn-sm btn-decree change-quantity" href="#" data-action="dec"
                           data-id="<%=c.getId()%>"><i
                                class="fas fa-minus-square"></i></a>
                        <input type="text" name="quantity" class="form-control w-50 quantity"
                               value="<%=c.getQuantity()%>" readonly>
                        <a class="btn btn-sm btn-inc change-quantity" href="#" data-action="inc"
                           data-id="<%=c.getId()%>"><i
                                class="fas fa-plus-square"></i></a>
                    </div>
                </form>
            </td>
            <td><a class="btn btn-sm btn-danger" href="#" data-id="<%=c.getId()%>">Xóa</a></td>
        </tr>
        <% }
        }
        %>
        </tbody>
    </table>
</div>
<div class="container">
    <div class="row">
        <button class="btn btn-warning"><a href="<%=request.getContextPath()%>/Category">TIẾP TỤC MUA SẮM</a></button>
    </div>
</div>
<jsp:include page="/views/footer.jsp"/>

<script>
    $(document).ready(function () {
        // Xử lý tăng/giảm số lượng
        $('.change-quantity').on('click', function (e) {
            e.preventDefault();
            var action = $(this).data('action');
            var id = $(this).data('id');
            var $quantityInput = $(this).closest('form').find('input[name="quantity"]');
            $.ajax({
                url: '<%=request.getContextPath()%>/QuantityInDeCre',
                type: 'GET',
                data: {
                    action: action,
                    id: id
                },
                success: function (response) {
                    var newQuantity = response.newQuantity;
                    var newTotal = response.newTotal;
                    $quantityInput.val(newQuantity);
                    $('#totalPrice').text(Intl.NumberFormat('vi-VN', {
                        style: 'currency',
                        currency: 'VND'
                    }).format(newTotal));
                },
                error: function () {
                    alert('Có lỗi xảy ra, vui lòng thử lại.');
                }
            });
        });

        // Xử lý xóa sản phẩm
        $('.btn-danger').on('click', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            var $row = $(this).closest('tr');

            $.ajax({
                url: '<%=request.getContextPath()%>/RemoveItemCart',
                type: 'GET',
                data: {
                    id: id
                },
                success: function (response) {
                    var newTotal = response.newTotal;
                    var newItemCount = response.newItemCount;

                    $row.remove();
                    $('#totalPrice').text(Intl.NumberFormat('vi-VN', {
                        style: 'currency',
                        currency: 'VND'
                    }).format(newTotal));

                    $('#cartItemCount').text(newItemCount);
                },
                error: function () {
                    alert('Có lỗi xảy ra, vui lòng thử lại.');
                }
            });
        });


    });
</script>
</body>
</html>
