<%@ page import="model.Order" %>
<%@ page import="java.util.ArrayList" %>
<%@ page import="dao.userDAO.OrderDao" %>
<%@ page import="java.util.Comparator" %>
<%@ page import="model.User" %>
<%@ page import="java.util.Map" %>
<%@ page import="model.Product" %>
<%@ page import="service.manageUser.order.OrderService" %><%--
  Created by IntelliJ IDEA.
  User: ADMIN
  Date: 15/7/2024
  Time: 11:36
  To change this template use File | Settings | File Templates.
--%>
<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<html>
<head>
    <title>Đơn hàng của tôi</title>
</head>
<body>
<% User u = (User) session.getAttribute("user");
    if(u==null)
        u = new User();
%>
<jsp:include page="../../header.jsp"/>
<section class="container">
    <!-- sản phẩm đã mua -->
    <div id="Order" class=" " >
        <!-- menu -->
        <div class="row ">
            <ul class="nav nav-tabs">
                <li class="nav-item">
                    <button id="nav-all" class="navButton nav-link active" aria-current="page"
                    >Tất cả
                    </button>
                </li>
                <li class="nav-item">
                    <button id="nav-waitingPay" class="navButton nav-link" href="#">Chờ xác nhận
                    </button>
                </li>
                <li class="nav-item">
                    <button id="nav-noPayment" class="navButton nav-link" href="#">Chưa thanh toán</button>
                </li>
                <li class="nav-item">
                    <button id="nav-paymentWas" class="navButton nav-link" href="#">Đã thanh toán</button>
                </li>

                <li class="nav-item">
                    <button id="nav-waitingForDelivery" class="navButton nav-link" href="#">chờ Giao
                        Hàng
                    </button>
                </li>
                <li class="nav-item">
                    <button id="nav-done" class="navButton nav-link" href="#">Hoàn thành</button>
                </li>
                <li class="nav-item">
                    <button id="nav-cancelled" class="navButton nav-link" href="#">Đã hủy</button>
                </li>
                <li class="nav-item">
                    <button id="nav-returnProduct" class="navButton nav-link" href="#">Trả hàng/Hoàn
                        tiền
                    </button>
                </li>
            </ul>
        </div>
        <!-- thanh tìm kiếm -->
        <div>
            <nav class="mt-3 bg-body-tertiary">
                <div class="container bg-white">
                    <div class="d-flex" role="search">
                        <input id="inputSearch" class="form-control me-2 w-100" type="search"
                               placeholder="Bạn có thể tìm kiếm sản phẩm theo tên hoặc id "
                               aria-label="Search" name="searchInput">
                        <button id="btnSearch" class="btn btn-outline-success" type="button">Search</button>
                    </div>
                </div>
            </nav>
        </div>
        <!-- danh sách sản phẩmphẩm -->
        <div id="danhSachSanPham" class="scrollable-div  w-100" style="max-height: 460px;min-height: 400px; overflow-x: hidden;overflow-y:auto  ">
            <%
                ArrayList<Order> orders = OrderDao.getInstance().selectOrderByidUser(u.getId(),"");
                orders.sort(new Comparator<Order>() {
                    @Override
                    public int compare(Order o1, Order o2) {
                        return o2.getId()-o1.getId();
                    }
                });
                int i = 0;
                for(Order o : orders){
                    i++;
                    if(i==11){
//                                break;
                    }
            %>
            <!-- 1 san pham -->
            <div class="product my-3 mt-2  border-top " id="">
                <%--                            status don hang--%>
                <div class="text-end border-bottom m-3 d-flex justify-content-between">
                    <span><STRONG>id Đơn hàng:</STRONG><%=o.getId()%></span>
                    <span><strong>Tình Trạng đơn hàng:</strong> <%=o.getStatus()%></span>
                    <span>Đơn hàng: <%=o.getStatusPayment()%></span>
                </div>

                <!-- list product -->
                <%
                    for(Map.Entry<Product,Integer> en : o.getProducts().entrySet()){
                %>
                <div class="row product ">
                    <div class="col-2 mx-auto ">
                        <img class=" "
                             src="<%=en.getKey().getImg_main()%>"
                             alt="" style="height: 100px; width: 100px;">
                    </div>
                    <div class="col-7 my-3">
                        <p class="mb-3"><Strong><%=en.getKey().getName()%></Strong></p>
                        <span class="align-text-bottom">
                                            <p class="">số lượng: <%=en.getValue()%></p>
                                        </span>

                    </div>
                    <p class="col-2 text-end mx-auto "><%=OrderService.getInstance().formatNumber( en.getKey().getPrice()*en.getValue())%> </p>
                    <p class="border  w-75 mx-auto fw-bold"></p>
                </div>
                <%
                    }
                %>

                <!-- thanh tien -->
                <div class="d-flex justify-content-between">
                    <p class="text-end mx-4"><Strong> Thành tiền:</Strong> <%=OrderService.getInstance().formatNumber(o.getTotal_price())%></p>
                    <form action="../../../showPayment" method="post">
                        <input type="hidden" name="orderID" value="<%=o.getId()%>">
                            <button type="submit" class="btn btn-info">Xem trang thanh toán</button>
                    </form>
                </div>
            </div>
            <%
                }
            %>
        </div>

    </div>
</section>

<jsp:include page="../../footer.jsp"/>
<script>
    // chuyen tap
    $('.navButton').click(function (){
        tapId= $(this).attr('id')
        var data = {
            data: tapId
        }
        $('.navButton').removeClass('active')
        $(this).addClass('active')
        $('#inputSearch').val("")

        fetchListOrder(data)
    })

    function fetchListOrder(data){
        $.ajax({
            url:'../../../getOrderByStatus',
            type:'GET',
            data: data,
            success: function (response){
                $('#danhSachSanPham').empty()
                $('#danhSachSanPham').html(response)
            },
            error: function (){
                alert('that bai')
            }
        })
    }
    //  search order
    var tapId = 'nav-all';
    $('#btnSearch').click(function (){
        var key = $('#inputSearch').val();
        var data ={
            data: key,
            tapId: tapId
        }
        $.ajax({
            url:'../../../searchOrder',
            type: 'POST',
            data: data,
            success: function (resp){
                $('#danhSachSanPham').empty()
                $('#danhSachSanPham').html(resp)
            },
            error: function (){
                alert('Tìm Kiếm Thất bại')
            }
        })
    })


    $(document).ready(function (){
        nav_active()
    })
    function nav_active() {
        $('a.nav-link').removeClass('active')
    }
</script>
</body>
</html>
