using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebBanHangOnline.Extention;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.ProductModels;
using WebBanHangOnline.ViewModels;

namespace WebBanHangOnline.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly QlbanHangContext db = new QlbanHangContext();


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

        /*
         1. Thêm mới sp vào giỏ hàng 
         2. Cập nhật lại sl sp trong giỏ hàng
         3. Xóa sp khỏi giỏ hàng
         4. Xóa luôn giỏ hàng

        */
        [HttpPost]
        [Route("/api/cart/add")]
        public IActionResult ThemVaoGioHang(string maSP, int? soLuong)
        {
            List<CartItem> gioHang = GioHang;

            try
            {
                //Them sp vao gio hang
                CartItem item = GioHang.SingleOrDefault(p => p.product.MaSp == maSP);
                if (item != null) //da co --> cap nhat so luong
                {
                    if (soLuong.HasValue)
                    {
                        item.amount = soLuong.Value;
                    }
                    else
                    {
                        item.amount++;
                    }
                }
                else
                {
                    DanhMucSp hh = db.DanhMucSps.SingleOrDefault(p => p.MaSp == maSP);
                    item = new CartItem
                    {
                        amount = soLuong.HasValue ? soLuong.Value : 1,
                        product = hh
                    };
                    gioHang.Add(item);//them vao gio
                }

                //Luu lai session
                HttpContext.Session.Set<List<CartItem>>("GioHang", gioHang);
                return Json(new { success = true });
            }
            catch
            {
                return Json(new { success = false });
            }
        }

        [HttpPost]
        [Route("/api/cart/update")]
        public IActionResult CapNhatGioHang(string maSP, int? soLuong)
        {
            //lay gio hang ra de xu ly
            var cart = HttpContext.Session.Get<List<CartItem>>("GioHang");
            try
            {
                if (cart != null)
                {
                    CartItem item = cart.SingleOrDefault(p => p.product.MaSp == maSP);
                    if (item != null && soLuong.HasValue)//da co --> cap nhat so luong
                    {
                        item.amount = soLuong.Value;
                    }
                    else
                    {
                        item.amount++;
                    }
                    //luu lai session
                    HttpContext.Session.Set<List<CartItem>>("GioHang", cart);
                }
                return Json(new { success = true });
            }
            catch
            {
                return Json(new { success = false });
            }
        }

        [HttpPost]
        [Route("/api/cart/remove")]
        public IActionResult XoaGioHang(string maSP)
        {
            List<CartItem> gioHang = GioHang;
            try
            {
                CartItem item = gioHang.SingleOrDefault(p => p.product.MaSp == maSP);
                if (item != null)
                {
                    gioHang.Remove(item);
                }
                //Luu lai session
                HttpContext.Session.Set<List<CartItem>>("GioHang", gioHang);
                return Json(new { success = true });
            }
            catch
            {
                return Json(new { success = false });
            }
        }
        [Route("giohang")]
        public IActionResult Index()
        {

            return View(GioHang);
        }
    }
}
