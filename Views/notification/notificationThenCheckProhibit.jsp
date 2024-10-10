<%--
  Created by IntelliJ IDEA.
  User: ngoke
  Date: 7/12/2024
  Time: 6:14 PM
  To change this template use File | Settings | File Templates.
--%>
<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<html>
<head>
    <title>Thông báo</title>
</head>
<body>
<h1>Tài khoản của bạn đã bị khóa!!!Vui lòng liên hệ Admin,sau 3 giây sẽ quay về trang đăng nhập</h1>
</body>
<script>
    setTimeout(function (){
        window.location.href = '<%=request.getContextPath()%>/views/user/login.jsp'
    },3000)
</script>
</html>
