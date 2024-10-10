<%@ page import="model.Product" %>
<%@ page import="java.util.List" %>
<%@ page import="java.text.DecimalFormat" %>
<%@ page import="dao.userDAO.ProductDao" %>
<%@ page import="connector.DAOConnection" %>
<%@ page import="model.Cart" %>
<%@ page import="java.util.ArrayList" %>
<%@ page import="java.sql.SQLException" %>
<%@ page import="java.util.Locale" %>
<%@ page import="java.text.NumberFormat" %>
<%@ page import="model.Category" %>
<%@ page import="model.User" %>
<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<%@ taglib uri="http://java.sun.com/jsp/jstl/core" prefix="c" %>
<%
    NumberFormat format = NumberFormat.getInstance(new Locale("vn", "VN"));
    ProductDao dao = new ProductDao(DAOConnection.getConnection());
    List<Product> listProduct = dao.getAllProduct();
    String name = (String) request.getAttribute("name");
    ArrayList<Cart> cart_list = (ArrayList<Cart>) session.getAttribute("cart-list");
    List<Cart> cartProduct = dao.getCartProduct(cart_list);
    if (cart_list != null) {
        request.setAttribute("cart_list", cart_list);
    }

%>
<html>
<head>
    <title>DANH MỤC :<%=name%>
    </title>
    <link href="<%=request.getContextPath()%>/css/cate.css" rel="stylesheet" type="text/html">
</head>
<body>
<jsp:include page="../../header.jsp"/>
<div class="text" style="text-align: center;margin-top: 15px">
    <h3 style="color: #555555; ">DANH SÁCH SẢN PHẨM:<%=name.toUpperCase()%>
    </h3>
</div>
<div class="container" id="product-list">
    <div class="row">
        <%
            List<Product> listS = (List<Product>) request.getAttribute("listCat");
            for (Product p : listS) {
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
                        <p><%= format.format(p.getPrice())%>đ
                        </p>
                    </h6>
                    <div class="mt-3 d-flex justify-content-between">
                        <form action="<%=request.getContextPath()%>/order" method="Post" id="<%=p.getId()%>">
                            <input id="ipPro" type="hidden" name="id" value="<%=p.getId()%>">
                            <input id="numPro" type="hidden" name="num" value="1">
                            <button id="btnBuy" class="btnBuy btn btn-primary btn-custom" type="button">Mua ngay</button>
                        </form>
                        <div>
                            <a class="btn btn-dark btn-custom add-to-cart" href="#"
                               data-id="<%=p.getId()%>" style="padding: 5px 10px;font-size: 15px;white-space: nowrap">Giỏ hàng</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <% }
        %>
    </div>
    <!-- Phân trang -->
    <%
        Integer id = (Integer) request.getAttribute("id");
//        Category c = new Category();
    %>
    <div class="text-center">
        <nav aria-label="Page">
            <ul class="pagination justify-content-center">
                <c:forEach var="i" begin="1" end="${ endPage }">
                    <li class="${status == i?"active":""}">
                        <a class="page-link pagination-link"
                           href="<%= request.getContextPath() %>/ProductByCategory?name=<%=name%>&id=<%=id%>&indexP=${i}">${i}</a>
                    </li>
                </c:forEach>
            </ul>
        </nav>
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
    });
</script>
</body>
</html>
