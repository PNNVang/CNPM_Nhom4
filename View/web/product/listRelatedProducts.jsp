<%@ page import="model.Product" %><%--
  Created by IntelliJ IDEA.
  User: ADMIN
  Date: 21/5/2024
  Time: 16:19
  To change this template use File | Settings | File Templates.
--%>
<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<!-- link bootstrap -->
<style>
    .list-product {
        width: 1080px;
    }

    .list-product .product {
        width: 200px;
    }

    .list-product .product img {
        height: 200px;
        width: 200px;
    }

    .btn-d-none {
        font-size: 13px;
        height: 30px;
        width: 80px;
        text-align: center;
        line-height: 30px;
        /* Căn giữa theo chiều dọc */
        display: none;
        background-color: #A80F23;
        color: #ffffff;
        animation: moveUp 1s ease forwards;
    }

    .product:hover .btn-d-none {
        display: block;
    }

    .product .btn-d-none:hover {
        border-width: 2px;
        border-style: dashed;
        border-color: black;
        color: #A80F23;
        background-color: #ffffff;
    }

    @keyframes moveUp {
        0% {
            opacity: 0;
            transform: translateY(100%)
        }

        100% {
            opacity: 1;
            transform: translateY(0)
        }
    }
</style>
<script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
<%
    Product p = (Product) request.getAttribute("product");
%>
<section class=" list-product container-xl mt-3 text-center fs-3 ">
    <p class="border-bottom ">Sản phẩm liên quan</p>
    <!-- product -->
    <div class="row" id="listProduct">

    </div>
    <script>
        var cate_id = '<%=p.getCategory_id()%>';
        var product_id = '<%=p.getId()%>';

        $(document).ready(function (){
         $.ajax({
             type: 'POST',
             url: "relatedProduct",
             data: {
                 category_id : cate_id,
                 product_id : product_id
             },
             success: function (responseText) {
                 $('#listProduct').html(responseText);
             },
             error: function (xhr, status, error) {
                 console.error('An error occurred:', error);
             }
         });
     });
    </script>
</section>
