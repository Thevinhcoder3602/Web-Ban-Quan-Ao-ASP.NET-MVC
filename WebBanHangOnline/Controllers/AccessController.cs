using Microsoft.AspNetCore.Mvc;
using WebBanHangOnline.Models;
using BCrypt.Net;

namespace WebBanHangOnline.Controllers
{
    public class AccessController : Controller
    {
        QlbanHangContext db = new QlbanHangContext();
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
                 && x.Password.Equals(user.Password)).FirstOrDefault();
                if (u != null)
                {
                    HttpContext.Session.SetString("UserName", u.Username.ToString());
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.LoginFail = "Tài khoản hoặc mật khẩu không đúng. Vui lòng kiểm tra lại !";
                    return View("Login");
                }
            }
            return View();
        }

        public IActionResult Logout()
        {

            HttpContext.Session.Clear();
            HttpContext.Session.Remove("UserName");
            return RedirectToAction("Login", "Access");
        }

    }
}