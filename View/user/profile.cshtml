<%@ page import="model.User" %>
<%@ page import="dao.userDAO.OrderDao" %>
<%@ page import="model.Order" %>
<%@ page import="java.lang.reflect.Array" %>
<%@ page import="java.util.ArrayList" %>
<%@ page import="java.util.Comparator" %>
<%@ page import="service.manageUser.order.OrderService" %>
<%@ page import="model.Product" %>
<%@ page import="java.util.Map" %><%--
  Created by IntelliJ IDEA.
  User: ADMIN
  Date: 20/3/2024
  Time: 21:37
  To change this template use File | Settings | File Templates.
--%>
<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<html>
<head>
    <title>Trang cá nhân</title>

    <link rel="stylesheet" href="${pageContext.request.contextPath}/css/profile.css" type="text/css">

</head>
<body>
<% User u = (User) session.getAttribute("user");
if(u==null)
    u = new User();
String gender = u.getGender();
%>
<jsp:include page="../header.jsp"/>
<section id="profile" class="mb-5">
    <div class="container ">
        <div class="row">
            <!-- menu bar -->
            <div class="col-3 ">
                <div class="row p-3">
                    <div class="avatar d-flex mb-2">
                        <div class="img-thrum">
                            <img src="<%=u.getAvatar()%>" alt="" class="rounded-5 border">
                        </div>
                        <div class=" d-flex  ms-3">
                            <p class="m-auto "><strong><%=u.getusername()%></strong></p>
                        </div>
                    </div>
                    <ul class="context-profile list-group list-group-flush">
                        <li class="list-group-item" style="background-color: #F5F5F5;">
                            <button id="btnProfile"
                                    class="btn actived">Hồ sơ của tôi
                            </button>
                        </li>


                    </ul>
                </div>
            </div>
            <!-- main context -->
            <div class="col-9 border-start " style="background-color: white;">
                <!-- hồ sơ cá nhân -->
                <div id="ProfileInfo">
                    <div class="row border-bottom">
                        <Span class="fs-3">Hồ sơ Của tôi</Span>
                    </div>
                    <div>
                        <form action="../../profile" method="POST" enctype="multipart/form-data" accept-charset="UTF-8">
                            <div class="row">
                                <div class="col-8">
                                    <!-- fill information -->
                                    <div class="">
                                        <div class="row my-2">
                                            <p class="col-4 text-end ">Tên đăng nhập:</p>
                                            <p class="col-8"><%=u.getusername()%>
                                            </p>
                                        </div>
<%--                                        Ho Ten--%>
                                        <div class="row my-2">
                                            <p class="col-4 text-end">Họ và tên:</p>
                                            <p class=" col-8"><% if (u.getFullName() == "" || u.getFullName() == null) { %>
                                                <input class=" w-100" type="text" name="fullName"
                                                       value="">
                                                <%} else { %>
                                                <%=u.getFullName()%>
                                                <%}%>
                                            </p>
                                        </div>
<%--                                        email--%>
                                        <div class="row my-2">
                                            <p class="col-4 text-end">Email:</p>
                                            <div class="col-8 d-flex">
                                                <p id="showEmail" class="w-75"><%=u.getEmail()%></p>
                                                <button type="button" id="btnChangeEmail"
                                                        class="btn btn-link  p-0  ms-3" style="height: 24px;">
                                                    <i class="fas fa-edit"></i>
                                                </button>
                                            </div>
                                        </div>
<%--                                        so dien thoai--%>
                                        <div class="row my-2">
                                            <p class="col-4 text-end">Số điện thoại:</p>
                                            <div class="col-8 d-flex">
                                                <p id="showPhoneNUmber" class="w-75"><%=u.getPhoneNumber()%></p>
                                                <input class="w-75" type="text" name="phoneNumber"
                                                       id="ipphoneNumber" value="" disabled>
                                                <button type="button" id="btnPhoneNumber" value="thay đổi"
                                                        class="btn btn-link  p-0 ms-3" style="height: 24px;">
                                                    <i class="fas fa-edit" id="iconPhoneNumber"></i></button>
                                            </div>
                                        </div>
<%--                                        dia chi--%>
                                        <div class="row my-2">
                                            <p class=" col-4 text-end">Địa chỉ:</p>
                                            <div class="col-8 d-flex">
                                                <p id="showAddress" class="w-75"><%=u.getAddress()%></p>
                                                <input type="text" class="w-75 " name="address" id="ipAddress"
                                                       value="" disabled>
                                                <button id="btnAddress" type="button" class="btn btn-link  p-0 ms-3"
                                                        style="height: 24px;"><i id="icAddress"
                                                                                 class="fas fa-edit"></i></button>
                                            </div>
                                        </div>
<%--                                        gioi tinh--%>
                                        <div class="row my-2">
                                            <p class=" col-4 text-end">Giới Tính:</p>
                                            <div class=" col-8 ">
                                                <div class="form-check form-check-inline">
                                                    <label class="form-check-label" for="gender_nu">Nữ</label>
                                                    <input class="form-check-input" type="radio" name="gender"
                                                           id="gender_nu" value="NU"
                                                           <%= "nu".equalsIgnoreCase(gender) ? "checked" : ""%>
                                                   >
                                                </div>
                                                <div class="form-check form-check-inline">
                                                    <label class="form-check-label"
                                                           for="gender_nam">Nam</label>
                                                    <input class="form-check-input" type="radio" name="gender"
                                                           id="gender_nam" value="Nam"
                                                        <%= "nam".equalsIgnoreCase(gender) ? "checked" : ""%>
                                                        >
                                                </div>
                                                <div class="form-check form-check-inline">
                                                    <label class="form-check-label"
                                                           for="gender_khac">khác</label>
                                                    <input class="form-check-input" type="radio" name="gender"
                                                           id="gender_khac" value="Khac"
                                                        <%= "khac".equalsIgnoreCase(gender) ? "checked" : ""%>>
                                                </div>
                                            </div>
                                        </div>
<%--                                        ngay sinh--%>
                                        <div class="row my-2">
                                            <p class="col-4 text-end">Ngày Sinh:</p>
                                            <div class=" col-8 d-flex"><input type="date" name="birthday"
                                                                              id="birthday" value="<%=u.getBirthday()%>"></div>
                                        </div>

                                    </div>
                                    <div class="text-center mt-3">
                                        <button class="btn btn-outline-primary" type="submit">Lưu</button>
                                    </div>
                                </div>
                                <div class="col-4 border-start">
                                    <div class="text-center my-5">
                                        <img id="imgAvatar" src="<%=u.getAvatar()%>" alt="">
                                    </div>
                                    <div class="mb-3 text-center">
                                        <input class="form-control d-inline" type="file" id="formFile" name="avatar">
                                    </div>
                                </div>
                            </div>
                        </form>
                    </div>
                </div>

            </div>
        </div>
    </div>
</section>
<jsp:include page="../footer.jsp"/>

<script>
    // Lấy tham chiếu đến các phần tử button và div
    // const BtnProfile = document.getElementById('btnProfile');
    // const btnOrder = document.getElementById('btnOrder');
    // const div1 = document.getElementById('ProfileInfo');
    // const div2 = document.getElementById('Order');
    // //
    // // Xử lý sự kiện khi nhấn vào button để vô hiệu hóa div tương ứng
    // BtnProfile.addEventListener('click', function () {
    //     BtnProfile.classList.add("actived")
    //     btnOrder.classList.remove("actived")
    //
    //     div1.style.display = "block";
    //     div2.style.display = "none";
    // });

    // btnOrder.addEventListener('click', function () {
    //     BtnProfile.classList.remove("actived")
    //     btnOrder.classList.add("actived")
    //
    //     div1.style.display = "none";
    //     div2.style.display = "inline";
    // });

    // thay đổi số điện thoại
    const btnPhoneNumber = document.getElementById('btnPhoneNumber');
    btnPhoneNumber.addEventListener('click', function () {
        const showPhoneNUmber = document.getElementById('showPhoneNUmber');
        const ipphoneNumber = document.getElementById('ipphoneNumber');
        const icon = document.getElementById("iconPhoneNumber");
        if (icon.classList.contains("fa-edit")) {

            showPhoneNUmber.style.display = "none";
            ipphoneNumber.value = showPhoneNUmber.textContent;

            ipphoneNumber.style.display = "block";
            ipphoneNumber.disabled = false;

            // Thiết lập giá trị mới cho button
            icon.classList.remove("fa-edit");
            icon.classList.add("fa-check");

        } else {
            if (isValidPhoneNumber(ipphoneNumber.value)) {
                showPhoneNUmber.style.display = "block";
                showPhoneNUmber.textContent = ipphoneNumber.value;

                ipphoneNumber.style.display = "none";
                ipphoneNumber.disabled = false;

                // Thiết lập giá trị mới cho button
                icon.classList.remove("fa-check");
                icon.classList.add("fa-edit");
            }
        }
    });
    // kiem tra so dien thoai
    function isValidPhoneNumber(phone) {
        // Biểu thức chính quy kiểm tra xem chuỗi chỉ chứa ký tự chữ cái hay không
        const pattern = /^(\+\d{1,3}[- ]?)?\d{10}$/;

        if (pattern.test(phone)) {

            return true;
        } else {
            alert('số điện thoại không hợp lệ.');
            return false;
        }
    }

    // thay đổi địa chỉ
    const btnAddress = document.getElementById('btnAddress');
    btnAddress.addEventListener('click', function () {
        const showAddress = document.getElementById('showAddress');
        const ipAddress = document.getElementById('ipAddress');
        const icon = document.getElementById("icAddress");
        if (icon.classList.contains("fa-edit")) {

            showAddress.style.display = "none";
            ipAddress.value = showAddress.textContent;

            ipAddress.style.display = "block";
            ipAddress.disabled = false;

            // Thiết lập giá trị mới cho button
            icon.classList.remove("fa-edit");
            icon.classList.add("fa-check");
        } else {
            showAddress.style.display = "block";
            showAddress.textContent = ipAddress.value;

            ipAddress.style.display = "none";
            ipAddress.disabled = false;

            // Thiết lập giá trị mới cho button
            icon.classList.remove("fa-check");
            icon.classList.add("fa-edit");
        }
    });


    // Lắng nghe sự kiện onchange của input file
    document.getElementById('formFile').onchange = function (event) {
        // Lấy tập tin được chọn
        const file = event.target.files[0];
        if (file && file.type.startsWith('image/')) {
            // Tạo đường dẫn cho hình ảnh
            const imageUrl = URL.createObjectURL(file);

            // Hiển thị hình ảnh trong phần tử img
            const imgPreview = document.getElementById('imgAvatar');
            imgPreview.src = imageUrl;
            imgPreview.style.display = 'block';
        } else {
            // Nếu tập tin không phải là hình ảnh, hiển thị thông báo lỗi
            alert('Vui lòng chọn một tập tin hình ảnh.');
        }
    };

    const gender = '<%=u.getGender()%>';
    var gender_nu =  document.getElementById('gender_nu');
    var gender_nam =  document.getElementById('gender_nam');
    var gender_khac =  document.getElementById('gender_khac');

    document.addEventListener('DOMContentLoaded', function() {
       if(gender === 'nu'){
            gender_nu.checked = true;
       }
        if(gender === 'nam'){
            gender_nam.checked = true;
        }
        if(gender === 'khac'){
            gender_khac.checked= true;
        }
    });
// xoa active tren menu
    $(document).ready(function (){
        nav_active()
    })
    function nav_active() {
        $('a.nav-link').removeClass('active')
    }

</script>
</body>
</html>
