using WebBanHangOnline.Models;
using Microsoft.AspNetCore.Mvc;
using WebBanHangOnline.Repository;
using WebBanHangOnline.ViewModels;
using Microsoft.AspNetCore.Http;
using WebBanHangOnline.Extention;

namespace WebBanHangOnline.Controllers.Component
{
    public class HeaderCartViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var cart = HttpContext.Session.Get<List<CartItem>>("GioHang");
            return View(cart);
        }
    }
}
