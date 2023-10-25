using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebBanHangOnline.Models;
using X.PagedList;


namespace WebBanHangOnline.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin")]
    [Route("admin/khachhang")]
    public class KhachHangController : Controller
    {
        QlbanHangContext db = new QlbanHangContext();

        public IActionResult Index()
        {
            return View();
        }
        [Route("ListKhachHang")]
        public IActionResult ListKhachHang(int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lst = db.KhachHangs.AsNoTracking().
                OrderBy(x => x.MaKhachHang);
            PagedList<KhachHang> list = new(lst,
                pageNumber, pageSize);
            return View(list);
        }
        [Route("ThemKhachHang")]
        [HttpGet]
        public IActionResult ThemKhachHang()
        {
            ViewBag.Username = new SelectList(db.Users.ToList(),
                "Username", "Username");
            return View();
        }
        [Route("ThemKhachHang")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemKhachHang(KhachHang khachHang)
        {
            if (ModelState.IsValid)
            {
                db.KhachHangs.Add(khachHang);
                db.SaveChanges();
                return RedirectToAction("ListKhachHang");
            }
            return View(khachHang);
        }
        [Route("ChiTietKhachHang")]
        public async Task<IActionResult> ChiTietKhachHang(string makh)
        {
            if (makh == null || db.KhachHangs == null)
            {
                return NotFound();
            }

            var Emlp = await db.KhachHangs
                .FirstOrDefaultAsync(m => m.MaKhachHang == makh);
            if (Emlp == null)
            {
                return NotFound();
            }

            return View(Emlp);
        }
        [Route("SuaKhachHang")]
        [HttpGet]
        public IActionResult SuaKhachHang(string makh)
        {
            ViewBag.Username = new SelectList(db.Users.ToList(),
                "Username", "Username");
            var KH = db.KhachHangs.Find(makh);
            return View(KH);
        }
        [Route("SuaKhachHang")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaKhachHang(KhachHang khachHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(khachHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ListKhachHang", "KhachHang");
            }
            return View(khachHang);
        }

        [Route("XoaKhachHang")]
        [HttpGet]
        public IActionResult XoaKhachHang(string makh)
        {
            TempData["Message"] = "";
            var acc = db.KhachHangs.Where(x => x.MaKhachHang == makh).ToList();
            db.Remove(db.KhachHangs.Find(makh));
            db.SaveChanges();
            TempData["Message"] = "Khách hàng đã được xóa";
            return RedirectToAction("ListKhachHang", "KhachHang");
        }

    }

}
