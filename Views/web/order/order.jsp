<%@ page import="model.Product" %>
<%@ page import="java.util.Map" %>
<%@ page import="model.User" %>
<%@ page import="java.util.HashMap" %>
<%@ page import="service.manageUser.order.OrderService" %><%--
  Created by IntelliJ IDEA.
  User: ngoke
  Date: 3/26/2024
  Time: 6:18 PM
  To change this template use File | Settings | File Templates.
--%>
<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<html>

<head>
    <title>Thanh toan</title>

</head>
<body>
<%
    Map<Product, Integer> listOrder = new HashMap<>();
    double total =0;
    OrderService orderService =  OrderService.getInstance();
    if(request.getSession(true).getAttribute("listOrder") != null){
        listOrder = (Map<Product, Integer>) request.getSession(true).getAttribute("listOrder");
    }

    if(request.getAttribute("totalPrice")!=null){
        total = (double) request.getAttribute("totalPrice");
    }
    User user = null;
    if(request.getSession(true).getAttribute("user") != null){
        user = (User) request.getSession(true).getAttribute("user");
    }
%>
<jsp:include page="../../header.jsp"/>
<section class=" " style="background-color: #eee;">
    <form action="checkOut" method="post">
        <div class="container py-1">
            <div class="row d-flex justify-content-center align-items-center ">
                <div class="col card">
                        <div class="card-body p-4 row">
                            <div class="col-lg-6">
                                <div class="text-center ">
                                    <h1 class="">Thanh Toán</h1>
                                </div>
                                <div class="accordion m-1" id="accordionPanelsStayOpenExample">
                                    <div class="accordion-item">
                                        <h2 class="accordion-header m-2">
                                            Thông Tin Khách Hàng
                                        </h2>
                                        <div id="collapse_Thong_tin_khach_hang" class="accordion-collapse collapse show">
                                            <div class="accordion-body">
                                                <!-- thong tin khach hang -->
                                                <div>
                                                    <div class="d-flex btn-group btn-group-toggle" data-toggle="buttons">
                                                        <div class="form-check form-check-inline">
                                                            <input class="form-check-input" type="radio"
                                                                   name="gender" id="gender_nam"
                                                                   value="Nam" required
                                                                <%if(user != null && user.getGender()!= null){%>
                                                                    <%=((user.getGender()).equalsIgnoreCase("nam"))? "checked":"" %>
                                                                <%}%>>
                                                            <label class="form-check-label" for="gender_nam">anh <span
                                                                    class="text-danger">*</span></label>
                                                        </div>
                                                        <div class="form-check form-check-inline">
                                                            <input class="form-check-input" type="radio"
                                                                   name="gender" id="gender_nu"
                                                                   value="Nu" required
                                                                <%if(user != null && user.getGender()!= null){%>
                                                                    <%=(user.getGender().equalsIgnoreCase("nu"))? "checked":"" %>
                                                                <%}%>>
                                                            <label class="form-check-label" for="gender_nu">chi<span
                                                                    class="text-danger">*</span></label>
                                                        </div>

                                                    </div>

                                                    <div class="row">
                                                        <div class="col">
                                                            <div class="mb-3">
                                                                <label for="nameCustommer" class="form-label">Họ và Tên:
                                                                </label>
                                                                <input type="text" class="form-control"
                                                                       id="nameCustommer"
                                                                       name="nameCustommer"
                                                                       placeholder="Họ và Tên (Bắt Buộc)"
                                                                <%
                                                                    if(user != null && user.getFullName()!= null){
                                                                %>
                                                                    value="<%=(!user.getFullName().equalsIgnoreCase("null"))?user.getFullName():""%>"
                                                                 <%
                                                                    }
                                                                 %>
                                                                       required >
                                                            </div>
                                                            <div class="mb-3">
                                                                <label for="email" class="form-label">Email:</label>
                                                                <input type="text" class="form-control" id="email"
                                                                       placeholder="email" name="email"
                                                                    <%
                                                                    if(user != null ){
                                                                %>
                                                                       value="<%=user.getEmail()%>"
                                                                    <%
                                                                    }
                                                                 %>
                                                                       required >
                                                            </div>
                                                        </div>

                                                        <div class="col">
                                                            <div class="mb-3">
                                                                <label for="phoneNumber" class="form-label">Số Điện
                                                                    Thoại</label>
                                                                <input type="text" class="form-control" id="phoneNumber"
                                                                       placeholder="Số Điện Thoại (Bắt Buộc)" name="phoneNumber" required
                                                                    <%
                                                                    if(user != null && user.getPhoneNumber()!= null){
                                                                    %>
                                                                       value="<%=!user.getPhoneNumber().equalsIgnoreCase("null")?user.getPhoneNumber():""%>"
                                                                    <%
                                                                    }
                                                                    %>
                                                                >
                                                            </div>
                                                            <div class="mb-3">
                                                                <label for="birthday" class="form-label">Ngày
                                                                    sinh</label>
                                                                <input type="text" class="form-control" id="birthday"
                                                                       placeholder="Ngày Sinh (dd-mm-yyyy)"
                                                                    <%
                                                                    if(user != null){
                                                                        if(user.getBirthday()!=null)
                                                                %>
                                                                       value="<%=user.getBirthday()%>"
                                                                    <%
                                                                    }
                                                                 %>
                                                                >
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="text-center my-2" id="containerbtnConfirmInfCostommer">
                                        <button class=" btn  btn-primary" type="button" value="true"
    <%--                                            data-bs-toggle="collapse" data-bs-target="#collapse_Thong_tin_khach_hang"--%>
    <%--                                            aria-expanded="true" aria-controls="collapse_Thong_tin_khach_hang"--%>
                                                id="btnConfirmInfCustommer">
                                            Tiếp tục
                                        </button>
                                    </div>
                                    <!-- Thong tin giao hang -->
                                    <div class="accordion-item">
                                        <h2 class="accordion-header">
                                            Hình Thức Nhận Hàng
                                        </h2>
                                        <!-- button lua chon loai giao hang   -->
                                        <div id="panelsStayOpen-collapseTwo" class="accordion-collapse collapse show">
                                            <div class="accordion-body">
                                                <div class="text-center">
                                                    <button type="button" class="col-5 mx-auto btn btn-outline-dark" id="btnGiaoHangTanNoi">
                                                        <p class="m-0"> <strong> Giao Hàng Tận Nơi</strong></p>
                                                        <p class="m-0">giao hàng toàn quốc</p>
                                                    </button>
                                                        <button type="button" class="col-5 mx-auto  btn btn-outline-dark" id="btnNhanTaiCuaHang">
                                                        <p class="m-0"><strong>Nhận Tại Cửa Hàng</strong></p>
                                                        <p class="m-0">Trả tiền lúc nhận hàng</p>
                                                        </button>
                                                </div>
                                                <div>
                                                    <!-- giao hang tan noi -->
                                                    <div class="row mt-4" id="GiaoHangTanNoi">
                                                            <div class="row">
                                                                <div class="col">
                                                                    <div class="form-outline form-white mb-4 row">
                                                                        <select id="citySeleted" name="city"
                                                                                class="form-control form-control-lg" required>
                                                                            <option value="">Chọn Tỉnh/Thành phố
                                                                            </option>
                                                                            <option value="1">Tỉnh/Thành phố 1</option>
                                                                            <option value="2">Tỉnh/Thành phố 2</option>
                                                                        </select>
                                                                    </div>
                                                                    <div class="form-outline form-white mb-4  row ">
                                                                        <select id="wardSeleted" name="wards"
                                                                                class="form-control form-control-lg"
                                                                                onclick="checkInputWard()" required>
                                                                            <option value="">Chọn Phường/Xã</option>
                                                                            <option value="1">Phường/Xã 1</option>
                                                                            <option value="2">Phường/Xã 2</option>
                                                                            <!-- Thêm các option khác tùy theo nhu cầu -->
                                                                        </select>
                                                                    </div>
                                                                </div>
                                                                <div class="col">
                                                                    <div class="form-outline form-white row mb-4 ">
                                                                        <select id="districtSeleted" name="district"
                                                                                class="form-control form-control-lg "
                                                                                 onclick="checkInputDistrict()" required>
                                                                            <option value="">Chọn Quận/Huyện</option>
                                                                            <option value="1">Quận/Huyện 1</option>
                                                                            <option value="2">Quận/Huyện 2</option>
                                                                            <!-- Thêm các option khác tùy theo nhu cầu -->
                                                                        </select>
                                                                    </div>

                                                                    <div class="form-outline form-white row mb-4">
                                                                        <div class="form-outline form-white">
                                                                            <input type="text" id="typeExp" name="address"
                                                                                   class="form-control form-control-lg"
                                                                                    minlength="7" maxlength="70" required />

                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row mb-4">
                                                                <div class="form-outline form-white">
                                                                    <input type="text" id="typeText_3"
                                                                           class="form-control form-control-lg"
                                                                           name="note"
                                                                           placeholder="Ghi Chú" size="1" minlength="10"
                                                                           maxlength="1000" />

                                                                </div>
                                                            </div>
                                                            <div>
                                                            <input id="accept_giaohangtannoi" type="checkbox" style="width: 20px; height: 20px;" required>
                                                            <span class="fs-4">Tôi chấp nhận chương tinh của trang web
                                                                </span>
                                                            </div>
                                                    </div>
                                                    <!-- nhan tai cua hang -->
                                                    <div class="row mt-4 d-none" id="NhanTaiCuaHang">
                                                        <p class="fs-4 fw-normal"> <strong>địa chỉ của hàng:</strong>
                                                            khu phố A phường Linh Trung Thành phố Thủ Đức</p>
                                                        <div class="row mb-4">
                                                            <div class="form-outline form-white">
                                                                <input type="text" id="typeText_2"
                                                                       name="note2"
                                                                       class="form-control form-control-lg"
                                                                       placeholder="Ghi Chú" size="1" minlength="10"
                                                                       maxlength="120" />

                                                            </div>
                                                        </div>
                                                        <div class="d-flex">
                                                            <input id="accept_nhantaicuahang" type="checkbox" style="width: 20px; height: 20px;"
                                                            name="accept_nhantaicuahang" value="accept">
                                                            <span class="fs-4 ms-4">xác nhận lấy hàng ở cửa hàng </span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-6">
                                <h5 class="mb-3"><a href="<%=request.getContextPath()%>/views/index.jsp" class="text-body"><i
                                        class="fas fa-long-arrow-alt-left me-2"></i> tiếp tục mua hàng</a></h5>
                                <hr>

                                <div class="d-flex justify-content-between align-items-center mb-4">
                                    <div>
                                        <p class="mb-1">Thông Tin Giỏ hàng</p>
                                    </div>
                                </div>
                                <!--  danh sach cac san pham -->
                                <div class="card mb-3 scrollable-div overflow-auto"   style="max-height: 270px">
                                    <div class="card-body">
                                        <%
                                            if(listOrder.size() == 0){
                                        %>
                                        <p>Bạn không có sản phẩm nào vui lồng thêm sản phẩm</p>
                                        <%
                                            }else{
                                         for(Map.Entry<Product,Integer> entry : listOrder.entrySet()){%>
                                        <%--product--%>
                                        <% Product p = entry.getKey(); %>
                                        <div class="d-flex justify-content-between" id="product_<%=p.getId()%>">

                                            <div class="d-flex flex-row align-items-center">
                                                <div>
                                                    <img src="<%=p.getImg_main()%>"
                                                         class="img-fluid rounded-3" alt="Shopping item"
                                                         style="width: 80px;">
                                                </div>
                                                <div class="ms-3" style="width: 200px">
                                                    <h6><%=p.getName()%></h6>
                                                </div>
                                            </div>
                                            <div class="d-flex flex-row align-items-center">
                                                <div style="width: 50px;">
                                                    <%--So luong--%>
                                                    <h5 class="fw-normal mb-0"><%=entry.getValue()%></h5>
                                                </div>
                                                <div style="width: 120px;">
                                                    <%--Gia --%>
                                                    <h5 class="mb-0"><%= orderService.formatNumber( entry.getValue()*p.getPrice())%></h5>
                                                </div>
                                                <button id="delete_product_<%=p.getId()%>" type="button" class="border-0 btn_delete_product" href="#" style="color: #cecece;"><i
                                                        class="fas fa-trash-alt" ></i></button>
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

                                <div class="d-flex justify-content-between">
                                    <p class="mb-2">Tổng số Tiền</p>
                                    <p class="mb-2"><%=orderService.formatNumber(total)%> Đ</p>
                                </div>

                                <div class="d-flex justify-content-between">
                                    <p class="mb-2">Phí ship</p>
                                    <p class="mb-2">20.000 Đ</p>
                                </div>

                                <div class="d-flex justify-content-between mb-4">
                                    <p class="mb-2">Thanh Toán</p>
                                    <p class="mb-2"><%=orderService.formatNumber(total-20000)%> Đ</p>
                                </div>
                                <button type="submit" class="btn btn-info btn-block btn-lg" id="btn">
                                    <div class="d-flex justify-content-between">
                                        <span>Đặt Hàng <i class="fas fa-long-arrow-alt-right ms-2"></i></span>
                                    </div>
                                </button>
                            </div>
                        </div>
                </div>
            </div>
        </div>
    </form>
</section>

<jsp:include page="../../footer.jsp"/>
</body>
<script>
    $(document).ready(function (){
        checkAndCollapse();
        fetchCitiesServlet();
    });
    // collapse yeu cau phai co ten va sdt
    $('#btnConfirmInfCustommer').click(function (e) {
        var fullName = $('#nameCustommer').val();
        var phoneNumber = $('#phoneNumber').val();
        if(fullName === "" || phoneNumber === ""){
            alert("Vui lòng nhập đầy đủ Họ và Tên và Số Điện Thoại.2");
            $('#collapse_Thong_tin_khach_hang').collapse('show')
        }else{
            if ($($(this)).val() === "true") {
                // dong colapse
                $('#collapse_Thong_tin_khach_hang').collapse('hide')

                $('#containerbtnConfirmInfCostommer').removeClass('text-center').addClass('text-end');
                $(this).addClass('btn-secondary').removeClass('btn-primary').text('Chỉnh sửa').val("false");
            } else {
                // hien thi colapse
                $('#collapse_Thong_tin_khach_hang').collapse('show')

                $('#containerbtnConfirmInfCostommer').removeClass('text-end').addClass('text-center');
                $(this).addClass('btn-primary').removeClass('btn-secondary').text('Tiếp tục').val("true");
            }
        }
    });
    // Hàm kiểm tra và đóng collapse nếu đã có giá trị
    function checkAndCollapse() {
        var name = $('#nameCustommer').val().trim();
        var phoneNumber = $('#phoneNumber').val().trim();

        if (name !== '' && phoneNumber !== '' && phoneNumber !== "null" ) {
            $('#collapse_Thong_tin_khach_hang').collapse('hide');
            $('#containerbtnConfirmInfCostommer').removeClass('text-center').addClass('text-end');
            $('#btnConfirmInfCustommer').addClass('btn-secondary').removeClass('btn-primary').text('Chỉnh sửa').val("false");
        }
    }
    //  xoa san pham
    $('.btn_delete_product').click(function (){
        $(this).addClass('border-2').removeClass('border-0')
        var id_product = $(this).attr('id').split('_')[2]

      $.ajax({
          type:'POST',
          url:'deleteProductInOrder',
          data:id_product,
          contentType: 'application/json',
          success: function (response){
              $('#product_'+id_product).remove()
          },
          error: function (xhr,status,error){

          }
      })

    })
    // chuyen giao dien lua chon giao hang tan noi
    $('#btnGiaoHangTanNoi').click(function () {
        $(this).addClass('')
        $('#GiaoHangTanNoi').removeClass('d-none');
        $('#NhanTaiCuaHang').addClass('d-none')
        $('#accept_giaohangtannoi').prop('required',true)
        $('#accept_nhantaicuahang').removeAttr('required')
        $('#accept_nhantaicuahang').prop('checked',false)


        $('#citySeleted').prop('required',true)
        $('#wardSeleted').prop('required',true)
        $('#districtSeleted').prop('required',true)
        $('#typeExp').prop('required',true)

        $('#btnNhanTaiCuaHang').removeClass('btn-dark').removeClass('text-light')
        $(this).addClass('btn-dark').addClass('text-light')


    });
    // chuyen giao dien lua chon nhan tai cua hang
    $('#btnNhanTaiCuaHang').click(function () {
        $('#GiaoHangTanNoi').addClass('d-none');
        $('#NhanTaiCuaHang').removeClass('d-none')
        $('#accept_nhantaicuahang').prop('required',true)
        $('#accept_giaohangtannoi').removeAttr('required')
        $('#accept_giaohangtannoi').prop('checked',false)


        $('#citySeleted').removeAttr('required')
        $('#wardSeleted').removeAttr('required')
        $('#districtSeleted').removeAttr('required')
        $('#typeExp').removeAttr('required')

        $('#btnGiaoHangTanNoi').removeClass('btn-dark').removeClass('text-light')
        $(this).addClass('btn-dark').addClass('text-light')
    });
    // tai danh sach thanh pho
    function fetchCitiesServlet(){
        var citySeleted = $('#citySeleted')
        $.ajax({
            url:'getListCity',
            type: 'GET',
            success: function (response){
                citySeleted.empty() // xoa cac tyu chon cu
                citySeleted.append('<option value="">Chọn Tỉnh/Thành phố</option>')
                var listCity
                try {
                    listCity = response
                }catch (e){
                    alert('Lỗi phân tích JSON: ' + e.message);
                }
                $.each(listCity,function (index,cityName){
                    citySeleted.append('<option value="' + cityName.id+ '">' + cityName.name + '</option>');
                })
            },
            error: function (error){
                alert('lay du lieu thanh pho that bai')
            }

        })
    };
    // nhan thong tin khi da chon city
    $('#citySeleted').change(function (){
        var city = $(this).val();
        if(city){
            fetchDistrictServlet(city)
        }else{

        }
    })
    // tai danh sach quan huyen
    function fetchDistrictServlet(city){
        $.ajax({
            type: 'GET',
            url: 'getListDistrict',
            data: {city:city},
            success: function (resp){
                var listDistrict;
                var districtSeleted = $('#districtSeleted')
                try{
                    listDistrict = resp;
                }catch (e){
                    alert('du lieu ko on')
                }
                districtSeleted.empty()
                districtSeleted.append('<option value="">Chọn Quận/Huyện</option>');
                $.each(listDistrict, function(index, district) {
                    districtSeleted.append('<option value="' + district.districtID+ '">' + district.districtName+ '</option>');
                });
            },
            error: function (error){
                alert('lay du lieu ko thanh cong')
            }
        })
    }
    // nhan su kien thay doi huyen
    $('#districtSeleted').change(function (){
        var district = $(this).val()
        if(district){
            fetchWardServlet(district)
        }
    });
    // tai danh sach xa
    function fetchWardServlet(district) {
        $.ajax({
            type:'Get',
            url: 'getListWard',
            data:{district:district},
            success: function (resp){
                var listWard;
                var wardSeleted = $('#wardSeleted');
                try{
                    listWard = resp
                }catch (e){
                    alert('du lieu ko on')
                }
                wardSeleted.empty()
                wardSeleted.append('<option value="">Chọn Xã,thị trấn</option>')
                $.each(listWard,function (index,ward){
                    wardSeleted.append('<option value="' + ward.id+ '">' + ward.wardName+ '</option>')
                })
            },
            error: function (error){
                alert('lay du lieu ko thanh cong')
            }
        })
    }
    // sau khi chon thanh pho roi moi con quan huyen
    function checkInputDistrict() {
        if ($('#citySeleted').val() === "") {
            alert("Bạn phải chọn thành pho trước");
        }
    }

    // //ham nay kiem tra gia tri trong o input phuong va lay du lieu tu servlet ve
    // function checkInputWard() {
    //     if ($('#districtSeleted').val() === "" || $('#citySeleted').val() === "") {
    //         alert("Bạn phải chọn theo thứ tự là thành phố trước, sau đó là quận/huyện");
    //     } else {
    //         $(document).ready(
    //             function () {
    //                 $.ajax({
    //                         url: '/getshippingfee',
    //                         method: 'GET',
    //                         dataType: 'Json',
    //                         data: {
    //                             city: input_name_city.value,
    //                             district: input_name_district.value,
    //                             ward: input_name_district.value
    //                         },
    //                         success: function (response) {
    //                             let announced_order = document.getElementById("announced-order")
    //                             announced_order.innerHTML += response
    //                         },
    //                         error: function (error) {
    //                             console.log('Lay du lieu khong thanh cong')
    //                         }
    //                     }
    //                 )
    //             }
    //         )
    //     }
    // }

</script>
</html>