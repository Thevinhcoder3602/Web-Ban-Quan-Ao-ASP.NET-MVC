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
        private readonly QlbanHangContext db = new QlbanHangContext();
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
                    ViewBag.UserName = u.Username;
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
       public IActionResult DangKyTaiKhoan(User user, RegisterVM kh)
        {
           if ( user != null )
            {
				// Băm mật khẩu trước khi lưu vào cơ sở dữ liệu
				string hashPassword = user.Password.ToMD5();
                 // Gán mật khẩu đã băm vào đối tượng User
                user.Password = hashPassword;

              

                // Tạo mã khách hàng tự động dựa trên số lượng khách hàng hiện có
                var khachHangCount = db.KhachHangs.Count() + 1;
				string maKhachHang = "KH" + khachHangCount.ToString("D3");
				// D3 để tạo mã có dạng KH001, KH002, ...
				


				// Thêm khách hàng mới vào bảng Khách hàng
				KhachHang khachhang = new KhachHang
				{
                    MaKhachHang = maKhachHang,
					TenKhachHang = kh.TenKhachHang,
					SoDienThoai = kh.SoDienThoai.Trim().ToLower(),
					Username = user.Username,
					Password = user.Password
				};
				db.KhachHangs.Add(khachhang);

				// Thêm Username và Password mới vào bảng User
				db.Users.Add(user);
                
				db.SaveChanges();
				return RedirectToAction("Login");

			}

            return RedirectToAction("DangKyTaiKhoan", "Access");
            
        }

        public IActionResult taikhoancuatoi()
        {
            var taikhoanID = HttpContext.Session.GetString("MaKhachHang");
            if (taikhoanID != null)
            {
                // Lấy thông tin khách hàng từ cơ sở dữ liệu dựa trên mã khách hàng (chuỗi)
                var khachhang = db.KhachHangs.AsNoTracking().SingleOrDefault(x => x.MaKhachHang == taikhoanID);
                if (khachhang != null)
                {
                    // Truyền thông tin khách hàng vào trang Dashboard để hiển thị
                    return View(khachhang);
                }
            }
            // Nếu không đăng nhập, chuyển hướng đến trang đăng nhập
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