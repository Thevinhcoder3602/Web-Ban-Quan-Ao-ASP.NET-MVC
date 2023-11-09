using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebBanHangOnline.Extention;
using WebBanHangOnline.Models;
using WebBanHangOnline.ViewModels;

namespace WebBanHangOnline.Controllers
{
    public class CheckoutController : Controller
    {
        QlbanHangContext db = new QlbanHangContext();
       
        public IActionResult Index(string? returnUrl = null)
        {
            var cart = HttpContext.Session.Get<List<CartItem>>("GioHang");
            var taikhoanID = HttpContext.Session.Get<List<CartItem>>("MaKhachHang");
            

            return View();
        }
        
        public List<CartItem> GioHang
        {
            get
            {
                var gh = HttpContext.Session.Get<List<CartItem>>("GioHang");
                if (gh == default(List<CartItem>))
                {
                    gh = new List<CartItem>(); 
                }
                return gh;
            }
        }

        
    }

    
}
