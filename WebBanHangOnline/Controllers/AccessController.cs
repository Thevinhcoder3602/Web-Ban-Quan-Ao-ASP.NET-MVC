using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebBanHangOnline.Extention;
using WebBanHangOnline.Helper;
using WebBanHangOnline.Models;
using WebBanHangOnline.ViewModels;

namespace WebBanHangOnline.Controllers
{
  
    public class AccessController : Controller
    {
        private  QlbanHangContext db = new QlbanHangContext();
        [HttpGet]

        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {

                var u = db.Users.Where(x => x.Username.Equals(user.Username)
                 && x.Password.Equals(user.Password.ToMD5())).FirstOrDefault();
                if (u != null)
                {
                    HttpContext.Session.SetString("UserName", u.Username.ToString());
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.LoginFail = "Thông tin đăng nhập không đúng";
                    return View("Login");
                }
            }
            return View();
        }

        [HttpGet]
      
        public IActionResult ValidatePhone(string sdt)
        {
            try
            {
                var khachhang = db.KhachHangs.AsNoTracking().SingleOrDefault(x => x.SoDienThoai.ToLower() == sdt.ToLower());
                if (khachhang != null)
                    return Json(data: "Số điện thoại : " + sdt + "đã được sử dụng");

                return Json(data: true);

            }
            catch
            {
                return Json(data: true);
            }
        }


        [HttpGet]
       
        [Route("DangKy", Name = "DangKy")]
        public IActionResult DangKyTaiKhoan()
        {
                return View();

        }
        [HttpPost]
      
        [Route("DangKy", Name = "DangKy")]
       public IActionResult DangKyTaiKhoan(User user)
        {
           if ( user != null )
            {
				// Băm mật khẩu trước khi lưu vào cơ sở dữ liệu
				string hashPassword = user.Password.ToMD5();
                 // Gán mật khẩu đã băm vào đối tượng User
                user.Password = hashPassword;
				// Thêm khách hàng mới vào cơ sở dữ liệu
				db.Users.Add(user);
				db.SaveChanges();
				return RedirectToAction("Login");

			}
            return RedirectToAction("DangKyTaiKhoan", "Access");
            
        }

        [Route("taikhoancuatoi")]
		public IActionResult Dashboard()
		{
			//var taikhoanID = HttpContext.Session.GetString("MaKhachHang");
			//if (taikhoanID != null)
			//{
			//	var khachhang = db.KhachHangs.AsNoTracking().SingleOrDefault(x => x.MaKhachHang == Convert.ToInt32(taikhoanID));
			//	if (khachhang != null)
			//	{
			//		var lsDonHang = db.Orders
			//			.Include(x => x.TransactStatus)
			//			.AsNoTracking()
			//			.Where(x => x.MaKhachHang == khachhang.MaKhachHang)
			//			.OrderByDescending(x => x.OrderDate)
			//			.ToList();
			//		ViewBag.DonHang = lstDonHang;
			//		return View(khachhang);
			//	}
			//}
			return RedirectToAction("Login");
		}
		public IActionResult Logout()
        {

            HttpContext.Session.Clear();
            HttpContext.Session.Remove("UserName");
            return RedirectToAction("Login", "Access");
        }

    }
}