using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebBanHangOnline.Models;
using X.PagedList;


namespace WebBanHangOnline.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin")]
    [Route("admin/homeadmin")]
    public class HomeAdminController : Controller
    {
        QlbanHangContext db = new QlbanHangContext();
        [Route("")]
        [Route("index")]
        
        public IActionResult Index()
        {
            return View();
        }
        [Route("danhmucsanpham")]
        public IActionResult DanhMucSanPham(int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstSanPham = db.DanhMucSps.AsNoTracking().
                OrderBy(x => x.TenSp);
            PagedList<DanhMucSp> lst = new PagedList<DanhMucSp>(lstSanPham,
                pageNumber, pageSize);
            return View(lst);
        }
        [Route("ThemSanPhamMoi")]
        [HttpGet]
        public IActionResult ThemSanPhamMoi()
        {
            ViewBag.MaChatLieu = new SelectList(db.ChatLieus.ToList(),
                "MaChatLieu", "ChatLieu");
            ViewBag.MaChatLieu = new SelectList(db.ChatLieus.ToList(),
                "MaHangSx", "HangSx");
            ViewBag.MaChatLieu = new SelectList(db.ChatLieus.ToList(),
                "MaLoai", "Loai");
            ViewBag.MaChatLieu = new SelectList(db.ChatLieus.ToList(),
                "MaChatLieu", "ChatLieu");
            return View();
        }
        [Route("ThemSanPhamMoi")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemSanPhamMoi(DanhMucSp sanPham)
        {
            if(ModelState.IsValid)
            {
                db.DanhMucSps.Add(sanPham);
                db.SaveChanges();
                return RedirectToAction("DanhMucSanPham");
            }
            return View(sanPham);
        }

        [Route("SuaSanPham")]
        [HttpGet]
        public IActionResult SuaSanPham(string maSanPham)
        {
            ViewBag.MaChatLieu = new SelectList(db.ChatLieus.ToList(),
                "MaChatLieu", "ChatLieu");

            ViewBag.MaChatLieu = new SelectList(db.ChatLieus.ToList(),
                "MaHangSx", "HangSx");

            ViewBag.MaChatLieu = new SelectList(db.ChatLieus.ToList(),
                "MaLoai", "Loai");

            ViewBag.MaChatLieu = new SelectList(db.ChatLieus.ToList(),
                "MaChatLieu", "ChatLieu");

            var sanPham = db.DanhMucSps.Find(maSanPham);
            return View(sanPham);
        }
        [Route("SuaSanPham")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaSanPham(DanhMucSp sanPham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sanPham).State = EntityState.Modified; //Update sp
                db.SaveChanges();
                return RedirectToAction("DanhMucSanPham","HomeAdmin");
            }
            return View(sanPham);
        }

        [Route("XoaSanPham")]
        [HttpGet]
        public IActionResult XoaSanPham(string maSanPham)
        {
            TempData["Message"] = "";
            var chiTietSanPham = db.ChiTietSps.Where(x => x.MaSp == maSanPham).ToList(); 
            if(chiTietSanPham.Count() > 0)
            {
                TempData["Message"] = "Không xóa được sản phẩm này";
                return RedirectToAction("DanhMucSanPham", "HomeAdmin");
            }
            var anhSanPham = db.AnhSps.Where(x => x.MaSp == maSanPham);
            if(anhSanPham.Any()) db.RemoveRange(anhSanPham);
            db.Remove(db.DanhMucSps.Find(maSanPham));
            db.SaveChanges();
            TempData["Message"] = "Sản phẩm đã được xóa";
            return RedirectToAction("DanhMucSanPham", "HomeAdmin");
        }


    }
  
}
