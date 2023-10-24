using WebBanHangOnline.Models;
using Microsoft.AspNetCore.Mvc;
using WebBanHangOnline.Repository;
using WebBanHangOnline.ViewModels;
using Microsoft.AspNetCore.Http;
using WebBanHangOnline.Extention;
namespace WebBanHangOnline.Controllers.Component
{
    public class NumberCartViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var cart = HttpContext.Session.Get<List<CartItem>>("GioHang");
            int soLuongSP = 0;
            if (cart != null)
            {
                soLuongSP = cart.Count;
            }
            return View(soLuongSP);
        }

    }
}
