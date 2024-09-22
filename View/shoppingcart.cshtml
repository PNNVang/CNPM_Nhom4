<%@ page import="model.User" %>
<%@ page import="java.util.ArrayList" %>
<%@ page import="model.Cart" %>
<%@ page import="java.util.List" %>
<%@ page import="dao.userDAO.ProductDao" %>
<%@ page import="connector.DAOConnection" %>
<%--
  Created by IntelliJ IDEA.
  User: ngoke
  Date: 4/1/2024
  Time: 5:28 PM
  To change this template use File | Settings | File Templates.
--%>
<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<% User auth = (User) request.getSession().getAttribute("user");
    if (auth != null) {
        request.setAttribute("user", auth);
    }
    ArrayList<Cart> cart_list = (ArrayList<Cart>) request.getSession().getAttribute("cart-list");
    List<Cart> cartProduct = null;
    if (cart_list != null) {
        ProductDao dao = new ProductDao(DAOConnection.getConnection());
        cartProduct = dao.getCartProduct(cart_list);
        double total = dao.getTotalCartPrice(cart_list);
        request.setAttribute("cart_list", cart_list);
        request.setAttribute("total", total);
    }
%>
<html>
<head>
    <title>Giỏ hàng</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet">
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.2/css/all.min.css" rel="stylesheet">
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
<div class="container">
    <jsp:include page="/views/header.jsp"/>
    <div class="d-flex py-3">
        <h3>Tổng tiền:${(total>0)?df.format(total):0}</h3>
        <a class="mx-3 btn btn-primary" href="#">Thanh toán</a>
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
                    <span><%=c.getName()%>></span>
                </div>
            </td>
            <td><%=c.getPrice()%>></td>
            <td>
                <form action="" method="post" class="form-inline d-flex justify-content-center">
                    <input type="hidden" name="id" value="<%=c.getId()%>"
                           class="form-input">
                    <div class="form-group d-flex justify-content-between w-50">
                        <a class="btn btn-sm btn-decree" href="QuantityInDeCre?action=dec&id=<%=c.getId()%>"><i
                                class="fas fa-minus-square"></i></a>
                        <input type="text" name="quantity" class="form-control w-50" value="<%=c.getQuantity()%>" readonly>
                        <a class="btn btn-sm btn-inc re" href="QuantityInDeCre?action=inc&id=<%=c.getId()%>"><i
                                class="fas fa-plus-square"></i></a>
                    </div>
                </form>
            </td>
            <td><a class="btn btn-sm btn-danger" href="RemoveItemCart?id<%=c.getId()%>"> Xóa</a></td>
        </tr>
        <% }
        }
        %>
        </tbody>
    </table>
</div>

</body>
<script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.11.8/dist/umd/popper.min.js"></script>
<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.min.js"></script>
<script src="https://code.jquery.com/jquery-3.2.1.min.js"></script>
</html>
