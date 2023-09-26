using Microsoft.AspNetCore.Mvc;
using WebBanHangOnline.Models;

namespace WebBanHangOnline.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin")]
    [Route("admin/homeadmin/dangnhap")]
    public class AccountController : Controller
    {
        QlbanHangContext db = new QlbanHangContext();
        [HttpGet]

        public IActionResult DangNhap()
        {
            if (HttpContext.Session.GetString("username") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("HomeAdmin","admin");
            }

        }
        [HttpPost]
        public IActionResult DangNhap(string user, string password)
        {
            var taiKhoan = db.Users.SingleOrDefault(x => x.Username.ToLower() == user.ToLower()
            && x.Password == password);
            if (taiKhoan != null)
            {
                HttpContext.Session.SetString("username", "taiKhoan");
                return RedirectToAction("HomeAdmin","admin");
            }
            else
            {
                TempData["error"] = "Tài khoản hoặc mật khẩu không đúng";
                return View();
            }    
         
        }

        public IActionResult DangXuat()
        {

            HttpContext.Session.Clear();
            HttpContext.Session.Remove("username");
            return RedirectToAction("DangNhap", "Account");
        }


    }
}
