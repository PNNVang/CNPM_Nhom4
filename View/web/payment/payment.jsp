<%@ page import="model.Product" %>
<%@ page import="service.manageUser.order.OrderService" %>
<%@ page import="java.util.Map" %>
<%@ page import="model.User" %>
<%@ page import="java.util.HashMap" %>
<%@ page import="model.Order" %><%--
  Created by IntelliJ IDEA.
  User: ADMIN
  Date: 6/7/2024
  Time: 15:15
  To change this template use File | Settings | File Templates.
--%>
<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<html>
<head>
    <title>Thanh Toan</title>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet">
    <link rel="preconnect" href="https://fonts.googleapis.com">
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
    <link href="https://fonts.googleapis.com/css2?family=Dancing+Script:wght@400;500;600;700&family=Montserrat:ital,wght@0,100;0,200;0,300;0,400;0,500;0,600;0,700;0,800;0,900;1,100;1,200;1,300;1,400;1,500;1,600;1,700;1,800;1,900&family=Roboto:ital,wght@0,300;0,400;0,500;0,900;1,100;1,300;1,400;1,500;1,700;1,900&display=swap"
          rel="stylesheet">
    <link href="https://fonts.googleapis.com/css2?family=Montserrat:ital,wght@0,100;0,200;0,300;0,400;0,500;0,600;0,700;0,800;0,900;1,100;1,200;1,300;1,400;1,500;1,600;1,700;1,800;1,900&family=Roboto:ital,wght@0,100;0,300;0,400;0,500;0,900;1,100;1,300;1,400;1,500;1,700;1,900&display=swap"
          rel="stylesheet">
    <link href="https://fonts.googleapis.com/css2?family=Playfair+Display:ital,wght@0,400;0,500;0,600;0,700;0,800;0,900;1,400;1,500;1,600;1,700;1,800;1,900&display=swap" rel="stylesheet">    <link rel="stylesheet" href="<%=request.getContextPath()%>/font/fontawesome-free-6.4.2/css/all.css">

</head>
<body>
<%
    Order order = (Order) request.getSession().getAttribute("order");
    OrderService orderService =  OrderService.getInstance();
    Map<Product, Integer> listOrder = new HashMap<>();
    double total = 0;
    if(order != null){
        System.out.println(order);
        listOrder = order.getProducts();
        total = order.getTotal_price();
    }

    User user = null;
    if(request.getSession(true).getAttribute("user") != null){
        user = (User) request.getSession(true).getAttribute("user");
    }
%>
    <jsp:include page="../../header.jsp"/>
    <section style="min-height: 620px" class="container-sm ">
        <h2 class="text-center my-1" >Thanh Toán</h2>
<%--        <form action="payment" method="POST">--%>
            <div class="d-flex justify-content-center align-items-center">
            <div class="col-lg-8 text-center">
                <div class=" text-start">
                    <p> Thông tin người dùng:  <%=user.getFullName()%> (+84) <%=user.getPhoneNumber()%> <%=(order.getAddress()!=null)?order.getAddress():""%></p>
                    <p>Phương thức nhận Hàng: <%=order.getTypeShip().equalsIgnoreCase("Pick up at the shop")?" Nhận tại của hàng":" ship hàng"%></p>
                    <hr class="my-4  p">
                </div>

                <div class="d-flex justify-content-between" >
                    <div class="d-flex flex-row align-items-center">
                        <h6>Sản Phẩm</h6>
                        <div class="ms-3" style="width: 200px">
                            <h6>Tên Sản Phẩm</h6>
                        </div>
                    </div>
                    <div class="d-flex flex-row align-items-center">
                        <h6 class="fw-normal mb-0" style="width: 250px;">Số Lượng</h6>
                        <h6 style="width: 120px;" class="mb-0">Giá</h6>
                    </div>
                </div>
                <!--  danh sach cac san pham -->
                <div class="card mb-3 scrollable-div overflow-auto"   style="max-height: 180px">

                    <div class="card-body">
                        <%
                            if(listOrder == null){
                        %>
                        <p>Hiện không có sản phẩm nào </p>
                        <%
                        }else{
                            for(Map.Entry<Product,Integer> entry : listOrder.entrySet()){%>
                        <%--product--%>
                        <% Product p = entry.getKey(); %>
                        <% System.out.println(p);%>
                        <div class="d-flex justify-content-between" id="product_<%=p.getId()%>">

                            <div class="d-flex flex-row align-items-center">
                                <div>
                                    <img src="<%=p.getImg_main()%>"
                                         class="img-fluid rounded-3" alt="Shopping item"
                                         style="width: 80px;">
                                </div>
                                <div class="ms-3 text-start" style="width: 300px">
                                    <h6><%=p.getName()%></h6>
                                </div>
                            </div>
                            <div class="d-flex flex-row align-items-center">
                                <div style="width: 250px;">
                                    <%--So luong--%>
                                    <h5 class="fw-normal mb-0"><%=entry.getValue()%></h5>
                                </div>
                                <div style="width: 120px;">
                                    <%--Gia --%>
                                    <h5 class="mb-0"><%= orderService.formatNumber( entry.getValue()*p.getPrice())%></h5>
                                </div>

                            </div>
                        </div>
                        <%--end product--%>

                        <%
                                }
                            }
                        %>


                    </div>

                </div>
                <hr class="my-4">
                <p>
                    <span>phương Thức Thanh Toán:</span>
                <%
                    if(order.getStatusPayment().equalsIgnoreCase("đã thanh toán")){%>
                    <span> <%=order.getTypePayment()%> </span>
                    <h3> Thanh toán đã hoàn thành</h3>
                <%
                    }else{
                %>
                    <div class="btn-group" role="group" aria-label="Basic radio toggle button group">
                        <input  type="radio" class="btn-check " name="typePayment" id="tienMatpayment" autocomplete="off" value="tiền mặt"
                        >
                        <label class="btn btn-outline-info" for="tienMatpayment">Tiền Mặt</label>

                        <input type="radio" class="btn-check" name="typePayment" id="chuyenKhoanPayment" autocomplete="off" value="chuyển khoản" checked
                        >
                        <label class="btn btn-outline-info" for="chuyenKhoanPayment">Chuyển Khoản</label>
                    </div>
                <%}%>
                </p>
                <hr class="my-4">
                <%
                    if(order.getStatusPayment().equalsIgnoreCase("đã thanh toán")){
                %>
                <div class="d-flex justify-content-center align-items-center">
                <%
                    }else{
                %>
                <div class="d-flex justify-content-between align-items-center">
                <%
                    }
                %>
                    <div class="w-50">
                        <div class="d-flex justify-content-between">
                            <p class="mb-2">Tổng số Tiền</p>
                            <p class="mb-2"><%=orderService.formatNumber(total+20000)%> Đ</p>
                        </div>
                        <div class="d-flex justify-content-between">
                            <p class="mb-1">Phí ship</p>
                            <p class="mb-1">20.000 Đ</p>
                        </div>
                        <div class="d-flex justify-content-between mb-4">
                            <p class="mb-2">Xác nhận và Thanh Toán</p>
                            <p class="mb-2"><%=orderService.formatNumber(total)%> Đ</p>
                        </div>
                    </div>
                    <%
                        if(!order.getStatusPayment().equalsIgnoreCase("đã thanh toán")){
                    %>
                    <button id="btnSubmit" type="button" class="btn btn-info btn-block btn-lg" id="btn">
                        <div class="d-flex justify-content-between">
                            <span>Thanh Toan<i class="fas fa-long-arrow-alt-right ms-2"></i></span>
                        </div>
                    </button>
                    <%
                        }
                    %>
                </div>
            </div>
        </div>
<%--        </form>--%>

    </section>
    <jsp:include page="../../footer.jsp"/>
<script>
<%--    thanh toan--%>
        $('#btnSubmit').click(function (){
            var typePayment ;
            if($('#tienMatpayment').is(':checked')){
                typePayment = $('#tienMatpayment').val();
            }
            if($('#chuyenKhoanPayment').is(':checked')){
                typePayment = $('#chuyenKhoanPayment').val();
            }
            var data = {
                typePayment: typePayment
            }
            $.ajax({
                type:"POST",
                url: "payment",
                data: data,
                success: function (){
                        Swal.fire({
                        title: "Thông báo!",
                        text: "Bạn đã thanh toán thành công!",
                        icon: "success",
                        showCancelButton: true,
                        confirmButtonText: "ở lại trang",
                        cancelButtonText: "tiếp tục mua sắm"
                    }).then((result) =>{
                        if(result.isDismissed){
                            window.location.href = 'views/index.jsp'; // Điều hướng đến trang mua sắm (chỉnh sửa URL nếu cần)
                        }
                    })
                },
                error: function (){
                    alert('loi')
                }
            })
        })

</script>
</body>
</html>
