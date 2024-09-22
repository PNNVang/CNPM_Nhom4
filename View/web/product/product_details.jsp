<%@ page import="model.Product" %>

<%@ page import="java.util.Map" %>
<%@ page import="java.text.NumberFormat" %>
<%@ page import="java.util.Locale" %><%--

<%@ page import="model.Product_Detail" %>
<%@ page import="java.text.NumberFormat" %>
<%@ page import="java.util.Locale" %><%--

  Created by IntelliJ IDEA.
  User: ADMIN
  Date: 1/5/2024
  Time: 11:56
  To change this template use File | Settings | File Templates.
--%>
<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<html>
<head>
    <title>Chi Tiết sản phẩm</title>
    <!-- link bootstrap -->
    <style>
        #product-detail {}

        #product-detail .img-product img {
            height: 510px;
            width: 550px;
        }

        #product-detail .sub-img img {
            margin-top: 10px;
            height: 130px;
            width: 130px;
        }

        #product-detail .line {
            width: 100px;
            height: 1px;
            background-color: black;
        }

        #product-detail td {
            list-style-type: disc;
        }

        #product-detail .thanhToan button {
            height: 40px;
        }

        #largeImage {
            display: none;
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            z-index: 9999;
            background-color: rgba(0, 0, 0, 0.8);
        }

        #largeImage img {
            width: 650px; /* hoặc height: 800px; */
            height: auto; /* hoặc width: auto; */

        }

        #product-detail .dataFlow {
            margin: auto;
            display: flex;
        }

        #product-detail .dataFlow .key {
            font-weight: bold;
            flex: 2;
            margin: 0;
        }

        #product-detail .dataFlow .value {
            flex: 4;
            margin: 0;
        }
    </style>
</head>
<body>
<% NumberFormat numberFormat=NumberFormat.getCurrencyInstance(new Locale("vi","VN"));%>
<%
    Product p = (Product) request.getAttribute("product");
%>
<jsp:include page="../../header.jsp"/>
<section id="product-detail" class="container mt-2">
    <div class="container row mx-auto ">
        <div class="col-5 mx-4">
            <div class="img-product ">
                <img src="<%=p.getImg_main()%>"
                     alt="" onclick="displayIMage(this)">
            </div>
            <div class="d-flex">
                <div class="sub-img ms-2">
                    <img src="<%=p.getImg_extra1()%>"
                         alt="" onclick="displayIMage(this)">
                </div>
                <div class="sub-img ms-2">
                    <img src="<%=p.getImg_extra2()%>"
                         alt="" onclick="displayIMage(this)">
                </div>
                <div class="sub-img ms-2">
                    <img src="<%=p.getImg_extra3()%>"
                         alt="" onclick="displayIMage(this)">
                </div>
                <div class="sub-img ms-2 ">
                    <img src="<%=p.getImg_extra4()%>"
                         alt="" onclick="displayIMage(this)">

                </div>
            </div>

        </div>
        <div class="col-5 ms-3">
            <div class="name fs-3">
               <%=p.getName()%>
            </div>
            <div class="line"></div>

            <div class="gia fs-4 fw-bold my-4">
               <%= numberFormat.format(p.getPrice())%>
            </div>
            <div class="moTa">
<%--                <p class="lh-base fw-bold">--%>
<%--                    Ruby là một trong những lựa chọn hàng đầu để chế tác thành trang sức. Ruby tự nhiên có màu sắc từ đỏ--%>
<%--                    hồng đến đỏ sẫm – màu sắc mang vẻ đẹp quyến rũ hoàn mỹ được phái đẹp yêu thích.--%>
<%--                </p>--%>
                    <p class="lh-base fw-bold">
                       <%=p.getDescription()%>
                    </p>
                <ul>
                    <li>
                        <p class="fs-6">
                            Đá tự nhiên 100%
                        </p>
                    </li>
                    <li>
                        <p class="fs-6">
                            Kiểm định và bảo hành trọn đời
                        </p>
                    </li>
                    <li>
                        <p class="fs-6">
                            Miễn phí vận chuyển toàn quốc
                        </p>
                    </li>
                    <li>
                        <p class="fs-6">
                            Ship đảm bảo Quốc tế
                        </p>
                    </li>
                </ul>

            </div>

            <div class="row thanhToan">

                <button class="col me-2 btn btn-outline-warning fw-bold add-to-cart" data-id="<%=p.getId()%>">
                    Giỏ hàng
                </button>
                <form class="col" action="<%=request.getContextPath()%>/order" method="Post" id="<%=p.getId()%>">
                    <input id="ipPro" type="hidden" name="id" value="<%=p.getId()%>">
                    <input id="numPro" type="hidden" name="num" value="1">
                    <button id="btnBuy" class="btnBuy w-100  btn btn-outline-info fw-bold" type="button">Mua ngay</button>
                </form>

            </div>
        </div>
    </div>
    <!-- thông số kỹ thuật -->
    <%
        Map<String,String> info = p.getInfor();
    %>
    <div class="row container mx-auto  mt-3 ">
        <p class="text-center fs-3">
            THÔNG SỐ KỸ THUẬT
        </p>
        <div class="row d-flex ">
            <div class="col mx-auto ">
                <%
                    for(Map.Entry<String,String> entry : info.entrySet()) {
                %>
                <div class="dataFlow m-3 ">
                    <p class="key"> <%=entry.getKey().trim()%></p>
                    <p class="value"><%=entry.getValue().trim()%></p>
                </div>
                <%
                    }
                %>
            </div>
        </div>

    </div>
    <!-- sản phẩm liên quan -->
    <jsp:include page="listRelatedProducts.jsp"/>
</section>

<jsp:include page="../../footer.jsp"/>
<!-- ảnh được phóng to -->
<div id="largeImage" class="text-center"  onclick="hiddenIMage()">
    <img class="mt-5" src="" alt="Large Image" >
</div>
<script>
    var isClicked = "false";

    function displayIMage(element) {
        if (isClicked) {
            var imageURL = element.src;
            var largeImage = document.getElementById('largeImage');
            var imgElement = largeImage.querySelector('img');
            imgElement.src = imageURL;
            largeImage.style.display = 'block';
            isClicked = true
        }
    }
    function hiddenIMage(){
        var largeImage = document.getElementById('largeImage');
        largeImage.style.display = 'none';
    }

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
                            text:notification + ', ' +
                                'Số lượng bạn có thể mua:'+ res,
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
                url: '<%=request.getContextPath()%>/checkQuanlityProduct',
                type:'POST',
                data:{
                    productID : id,
                    numProduct : 1,
                },
                success: function (resp) {
                    var res = resp.num
                    var notification = resp.notification
                    // alert(notification)
                    if(notification === 'ok'){
                        $.ajax({
                            url: '<%=request.getContextPath()%>/AddToCartController',
                            type: 'GET',
                            data: {id: id},
                            success: function (response) {
                                if (response.status === "success") {
                                    swal.fire({
                                        icon: 'success',
                                        title: 'Thông báo',
                                        text: 'Sản phẩm đã được thêm vào giỏ hàng.'
                                    })
                                } else if (response.status === "exists") {
                                    swal.fire({
                                        icon: 'info',
                                        title: 'Thông báo',
                                        text: 'Sản phẩm đã tồn tại trong giỏ hàng.'
                                    })
                                } else {
                                    swal.fire({
                                        icon: 'warning',
                                        title: 'Thông báo',
                                        text: 'Có lỗi xảy ra, vui lòng thử lại.'
                                    })
                                }
                            },
                            error: function () {
                                swal.fire({
                                    icon: 'warning',
                                    title: 'Thông báo',
                                    text: 'Có lỗi xảy ra, vui lòng thử lại.'
                                })
                            }
                        });
                    }else{
                        swal.fire({
                            title: 'Thông Báo',
                            html: notification + '<br>Số lượng bạn có thể mua: ' + res,
                        })
                    }
                },
                error: function (){

                }
            });
        });
    });
</script>
</body>
</html>
