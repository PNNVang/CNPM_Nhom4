using Dot_Net_ECommerceWeb.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Dot_Net_ECommerceWeb.Controller;

public class AdminController:Microsoft.AspNetCore.Mvc.Controller
{
 public IActionResult admin()
 {
  return View();
 }

 public IActionResult admin_backward_product()
 {
  return View();
 }

 public IActionResult admin_form_update()
 {
  //check tempdata nếu còn thì sẽ gửi dữ liệu sang 
  //tempdata lưu trữ object chứa các thông tin cần hiển thị
  //các dữ liệu khác cũng vậy
  if (TempData["data"] != null)
  {
   var data = JsonConvert.DeserializeObject<Product>(TempData["data"].ToString());
   return View(data);
  }
  return View();
 }

 public IActionResult admin_form_upload_product()
 {
  return View();
 }

 public IActionResult admin_header()
 {
  return View();
 }

 public IActionResult admin_image()
 {
  return View();
 }

 public IActionResult admin_inventory()
 {
  return View();
 }

 public IActionResult admin_log()
 {
  return View();
 }

 public IActionResult admin_order()
 {
  return View();
 }

 public IActionResult admin_product()
 {
  return View();
 }

 public IActionResult admin_summary()
 {
  return View();
 }

 public IActionResult admin_user()
 {
  return View();
 }

 public IActionResult admin_editor()
 {
  return View();
 }

 public IActionResult admin_categories()
 {
  return View();
 }
}