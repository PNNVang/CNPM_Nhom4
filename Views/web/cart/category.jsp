<%@ page import="model.Product" %>
<%@ page import="java.util.List" %>
<%@ page import="java.text.DecimalFormat" %>
<%@ page import="model.Cart" %>
<%@ page import="dao.userDAO.ProductDao" %>
<%@ page import="connector.DAOConnection" %>
<%@ page import="java.util.ArrayList" %>
<%@ page import="model.Category" %>
<%@ page import="java.text.NumberFormat" %>
<%@ page import="java.util.Locale" %>
<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<%@ taglib uri="http://java.sun.com/jsp/jstl/core" prefix="c" %>
<%--
  Created by IntelliJ IDEA.
  User: admin
  Date: 5/19/2024
  Time: 1:13 PM
  To change this template use File | Settings | File Templates.
--%>

<%
    NumberFormat format = NumberFormat.getInstance(new Locale("vn", "VN"));
    ProductDao dao = new ProductDao(DAOConnection.getConnection());
    ArrayList<Cart> cart_list = (ArrayList<Cart>) request.getSession().getAttribute("cart-list");
    List<Cart> cartProduct = dao.getCartProduct(cart_list);
    if (cart_list != null) {
        request.setAttribute("cart_list", cart_list);
    }
%>

<!doctype html>
<html>
<head>
    <title>DANH MỤC SẢN PHẨM</title>
    <link href="<%=request.getContextPath()%>/css/cate.css" rel="stylesheet" type="text/html">
    <style>
        .cate {
            background-color: #f0f8ff;
            padding: 20px;
        }

        .categories .container-fluid {
            background-color: #ffffff; /* Màu nền bên trong */
            border-radius: 8px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
            padding: 20px;
        }

        .image-container {
            text-align: center;
            margin: 20px 0;
            background-color: #FFFAF0; /* Màu nền của vùng chứa hình ảnh và chữ */
            padding: 20px;
            border-radius: 8px;
            box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
            transition: background-color 0.3s ease;
        }

        .image-container:hover {
            background-color: #FFFAF0; /* Màu nền khi hover */
        }

        .image-container a {
            text-decoration: none;
            color: #333; /* Màu chữ ban đầu */
            transition: color 0.3s ease;
        }

        .image-container a:hover {
            color: #007bff; /* Màu chữ khi hover */
        }

        .category-image {
            width: 100%;
            height: 115px;
            border-radius: 8px; /* Hình ảnh bo tròn */
            transition: transform 0.3s ease;
        }

        .category-image:hover {
            transform: scale(1.05); /* Tăng kích thước khi hover */
        }

        .category-name {
            font-size: 1.1em;
            font-weight: bold; /* Làm cho chữ đậm hơn */
            margin-top: 10px;
            color: #555;
            transition: color 0.3s ease, font-weight 0.3s ease;
        }

        .category-name:hover {
            color: #007bff; /* Màu chữ khi hover */
            font-weight: bolder; /* Làm cho chữ đậm hơn khi hover */
        }

    </style>
</head>
<body>
<jsp:include page="../../header.jsp"/>
<div class="cate">
    <section class="categories">
        <div class="container-fluid">
            <div class="row justify-content-center" style="gap: 10px">
                <%
                    List<Category> list = (List<Category>) request.getAttribute("cate");
                    for (Category category : list) {
                %>
                <div class="col-6 col-md-2 image-container">
                    <a href="<%=request.getContextPath()%>/ProductByCategory?name=<%=category.getCategory_name()%>&id=<%=category.getId()%>">
                        <img class="category-image" src="<%=category.getImg()%>" alt="">
                        <p class="category-name"><%= category.getCategory_name()%>
                        </p>
                    </a>
                </div>
                <% } %>
            </div>
        </div>
    </section>
</div>
<div class="cate">
    <div class="container-fluid" id="product-list">
        <h3 style="text-align: center; font-size:50px;font-weight: bold ">Danh sách sản phẩm</h3>
        <div class="row">
            <% List<Product> listProduct = (List<Product>) request.getAttribute("listP");
                for (Product p : listProduct) {
            %>
            <div class="col-md-3 my-3">
                <div class="card w-100" style="width: 18rem;">
                    <a href="<%=request.getContextPath()%>/productDetail?id=<%=p.getId()%>">
                        <img class="card-img-top" src="<%=p.getImg_main()%>" alt="Product Image">
                    </a>
                    <div class="card-body">
                        <h5 class="card-title">
                            <p style="white-space: nowrap; overflow: hidden; text-overflow: ellipsis;"><%= p.getName()%>
                            </p>
                        </h5>
                        <h6 class="price">
                            <p><%= format.format(p.getPrice()) %>đ
                            </p>
                        </h6>
                        <div class="mt-3 d-flex justify-content-between">
<%--                            <a class="btn btn-primary btn-custom" href="#"--%>
<%--                               style="padding: 5px 10px;font-size: 15px;white-space: nowrap">Mua ngay</a>--%>
                            <form action="<%=request.getContextPath()%>/order" method="Post" id="<%=p.getId()%>">
                                <input id="ipPro" type="hidden" name="id" value="<%=p.getId()%>">
                                <input id="numPro" type="hidden" name="num" value="1">
                                <button id="btnBuy" class="btnBuy btn btn-primary btn-custom" type="button">Mua ngay</button>
                            </form>
                            <a class="btn btn-dark btn-custom add-to-cart" href="#"
                               data-id="<%=p.getId()%>" style="padding: 5px 10px;font-size: 15px;white-space: nowrap">Giỏ
                                hàng</a>
                        </div>
                    </div>
                </div>
            </div>
            <% } %>
        </div>
        <!-- Phân trang -->
        <div class="text-center">
            <nav aria-label="Page">
                <ul class="pagination justify-content-center">
                    <c:forEach var="i" begin="1" end="${ endP }">
                        <li class="${tag == i?"active":""}">
                            <a class="page-link pagination-link"
                               href="<%= request.getContextPath() %>/Category?id=${i}&index=${i}">${i}</a>
                        </li>
                    </c:forEach>
                </ul>
            </nav>
        </div>

    </div>
</div>

<jsp:include page="../../footer.jsp"/>
<script>
    $(document).ready(function () {
        // nhap so luong san pham
        $('.btnBuy').click(function (){
            var button = this
            Swal.fire({
                title: 'Nhập số lượng sản phẩm',
                input: 'number',
                inputValue: 1,
                showCancelButton: true,
                confirmButtonText: 'OK',
                cancelButtonText: 'Cancel',
                inputValidator: (value) => {
                    if (!value || value < 1) {
                        return 'Số lượng không hợp lệ';
                    }
                }
            }).then((result) => {
                if (result.isConfirmed) {
                    var numProduct = parseInt(result.value);
                    $(button).closest('form').find('#numPro').val(numProduct);
                    // $(button).attr('type','submit');
                    var idform = $(button).closest('form').attr('id');
                    checkQuanlityProduct(idform, numProduct)
                }
            });
        })
        // kiem tra so luong san pham con trong kho
        function checkQuanlityProduct(id, num){
            var form = $('#'+id)
            var productID = id;
            var numProduct = num;
            var data = {
                numProduct : numProduct,
                productID : productID
            }
            $.ajax({
                url: '<%=request.getContextPath()%>/checkQuanlityProduct',
                type: 'POST',
                data: data,
                success: function (resp) {
                    var res = resp.num
                    var notification = resp.notification
                    // alert(notification)
                    if(notification === 'ok'){
                        form.submit()
                    }else{
                        swal.fire({
                            title: 'Thông Báo',
                            html: notification + '<br>Số lượng bạn có thể mua: ' + res,
                        })
                    }
                },
                error: function (){
                    swal.fire({
                        title: 'Lỗi',
                        text: 'Đã xảy ra lỗi khi kiểm tra số lượng sản phẩm.',
                        icon: 'error'
                    });
                }
            })
            return false;
        }
        // them gio hang
        $(".add-to-cart").click(function (e) {
            e.preventDefault();
            var id = $(this).data("id");

            $.ajax({
                url: '<%=request.getContextPath()%>/AddToCartController',
                type: 'GET',
                data: {id: id},
                success: function (response) {
                    if (response.status === "success") {
                        swal.fire({
                            icon: 'success',
                            title:'Thông báo',
                            text:'Sản phẩm đã được thêm vào giỏ hàng.'
                        })
                    } else if (response.status === "exists") {
                        swal.fire({
                            icon: 'info',
                            title:'Thông báo',
                            text:'Sản phẩm đã tồn tại trong giỏ hàng.'
                        })
                    } else {
                        swal.fire({
                            icon: 'warning',
                            title:'Thông báo',
                            text:'Có lỗi xảy ra, vui lòng thử lại.'
                        })
                    }
                },
                error: function () {
                    swal.fire({
                        icon: 'warning',
                        title:'Thông báo',
                        text:'Có lỗi xảy ra, vui lòng thử lại.'
                    })
                }
            });
        });
        $(".pagination-link").click(function (e) {
            e.preventDefault(); // Ngăn chặn hành vi mặc định của thẻ <a>
            var url = $(this).attr("href");

            $.ajax({
                url: url,
                type: 'GET',
                success: function (response) {
                    // Cập nhật phần danh sách sản phẩm
                    var productListHtml = $(response).find("#product-list .row").html();
                    $("#product-list .row").html(productListHtml);

                    // Đánh dấu chỉ mục trang hiện tại là active
                    $(".pagination-link").parent().removeClass("active"); // Xóa active class khỏi tất cả các thẻ <li>
                    $(e.target).parent().addClass("active"); // Thêm active class vào thẻ <li> đã click

                    // Cuộn trang lên đầu
                    $('html, body').animate({
                        scrollTop: $("#product-list").offset().top
                    }, 500); // Thời gian cuộn là 500ms
                },
                error: function () {
                    alert("Có lỗi xảy ra, vui lòng thử lại.");
                }
            });
        });
    });
</script>
</body>
</html>
