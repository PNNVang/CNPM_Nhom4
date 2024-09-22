<%@ page import="model.Category" %>
<%@ page import="java.util.List" %>
<%@ page import="dao.CategoryDao" %><%--
  Created by IntelliJ IDEA.
  User: ADMIN
  Date: 14/7/2024
  Time: 17:24
  To change this template use File | Settings | File Templates.
--%>
<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<html>
<head>
    <title>Category</title>
</head>
<style>


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
        height: 150px;
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
<body>
<section class="categories">
    <h2 class="text-center mt-4">Danh mục sản phẩm</h2>

    <div class="container-fluid">
        <div class="row justify-content-center" style="gap: 10px">
            <%
                List<Category> list = CategoryDao.getInstance().listCategory();
                for (Category category : list) {
            %>
            <div class="col-6 col-md-2 image-container">
                <a href="<%=request.getContextPath()%>/ProductByCategory?name=<%=category.getCategory_name()%>">
                    <img class="category-image" src="<%=category.getImg()%>" alt="">
                    <p class="category-name"><%= category.getCategory_name()%>
                    </p>
                </a>
            </div>
            <% } %>
        </div>
    </div>
</section>

</body>
</html>
