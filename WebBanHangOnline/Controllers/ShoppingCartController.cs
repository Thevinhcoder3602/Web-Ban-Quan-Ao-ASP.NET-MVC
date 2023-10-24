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

        [HttpPost]
        [Route("/api/cart/add")]
        public IActionResult ThemVaoGioHang(string maSP, int? soLuong)
        {
            List<CartItem> gioHang = GioHang;

            try
            {
                //Them sp vao gio hang
                CartItem item = GioHang.SingleOrDefault(p => p.sanpham.MaSp == maSP);
                if (item != null)
                {
                    if (soLuong.HasValue)
                    {
                        item.soLuong = soLuong.Value;
                    }
                    else
                    {
                        item.soLuong++;
                    }
                }
                else
                {
                    DanhMucSp hh = db.DanhMucSps.SingleOrDefault(p => p.MaSp == maSP);
                    item = new CartItem
                    {
                        soLuong = soLuong.HasValue ? soLuong.Value : 1,
                        sanpham = hh
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
        [Route("/api/cart/remove")]
        public IActionResult XoaGioHang(string maSP)
        {
            List<CartItem> gioHang = GioHang;
            try
            {
                CartItem item = gioHang.SingleOrDefault(p => p.sanpham.MaSp == maSP);
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
