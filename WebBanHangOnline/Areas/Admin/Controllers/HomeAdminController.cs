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
        [Route("DanhMucSanPham")]
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
            if (ViewBag.MaChatLieu == null)
            {
                ViewBag.MaChatLieu = new SelectList(db.ChatLieus.ToList(), "MaChatLieu", "TenChatLieu");
            }
            ViewBag.MaHangSx = new SelectList(db.HangSxes.ToList(),
                "MaHangSx", "HangSx");

            ViewBag.MaLoai = new SelectList(db.LoaiSps.ToList(),
                "MaLoai", "Loai");

            ViewBag.MaNuocSx = new SelectList(db.QuocGia.ToList(),
                "MaNuoc", "TenNuoc");
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
            if (ViewBag.MaChatLieu == null)
            {
                ViewBag.MaChatLieu = new SelectList(db.ChatLieus.ToList(), "MaChatLieu", "TenChatLieu");
            }

            ViewBag.MaHangSx = new SelectList(db.HangSxes.ToList(),
                "MaHangSx", "HangSx");

            ViewBag.MaLoai = new SelectList(db.LoaiSps.ToList(),
                "MaLoai", "Loai");

            ViewBag.MaNuocSx = new SelectList(db.QuocGia.ToList(),
                "MaNuoc", "TenNuoc");

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
        [Route("ChiTietSanPham")]
        [HttpGet]
        public async Task<IActionResult> ChiTietSanPham(string id)
        {
            if (id == null || db.DanhMucSps == null)
            {
                return NotFound();
            }

            var danhMucSp = await db.DanhMucSps
                .FirstOrDefaultAsync(m => m.MaSp == id);
            if (danhMucSp == null)
            {
                return NotFound();
            }

            return View(danhMucSp);
        }

        [Route("XoaSanPham")]
        [HttpGet]
        public IActionResult XoaSanPham(string maSanPham)
        {
            TempData["Message"] = "";
            var chiTietSanPham = db.ChiTietSps.Where(x => x.MaSp == maSanPham).ToList();
            if (chiTietSanPham.Count() > 0)
            {
                TempData["Message"] = "Không xóa được sản phẩm này";
                return RedirectToAction("DanhMucSanPham", "HomeAdmin");
            }
            var anhSanPham = db.AnhSps.Where(x => x.MaSp == maSanPham);
            if (anhSanPham.Any()) db.RemoveRange(anhSanPham);
            db.Remove(db.DanhMucSps.Find(maSanPham));
            db.SaveChanges();
            TempData["Message"] = "Sản phẩm đã được xóa";
            return RedirectToAction("DanhMucSanPham", "HomeAdmin");
        }


    }
  
}
