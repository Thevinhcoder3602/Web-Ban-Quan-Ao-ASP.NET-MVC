using Microsoft.AspNetCore.Mvc;

namespace WebBanHangOnline.Controllers
{
    public class AjaxContenController : Controller
    {
        public IActionResult HeaderCart()
        {
            return ViewComponent("HeaderCart");
        }
        public IActionResult HeaderFavourites()
        {
            return ViewComponent("HeaderFavourites");
        }
    }
}
