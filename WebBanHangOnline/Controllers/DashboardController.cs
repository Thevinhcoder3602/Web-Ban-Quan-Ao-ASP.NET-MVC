using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBanHangOnline.Models;

namespace WebBanHangOnline.Controllers
{
    public class DashboardController : Controller
    {
        private QlbanHangContext db = new QlbanHangContext();

        public IActionResult Dashboard()
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
            return RedirectToAction("Dashboard");
        }
    }
}
