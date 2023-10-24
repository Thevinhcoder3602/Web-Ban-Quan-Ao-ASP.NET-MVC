using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebBanHangOnline.Models;
using X.PagedList;


namespace WebBanHangOnline.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin")]
    [Route("admin/sanpham")]
    public class SanPhamController : Controller
    {
        QLBanHangContext db = new QLBanHangContext();
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
                OrderBy(x => x.MaSp);
            PagedList<DanhMucSp> lst = new(lstSanPham,
                pageNumber, pageSize);
            return View(lst);
        }
        [Route("ThemSanPhamMoi")]
        [HttpGet]
        public IActionResult ThemSanPhamMoi()
        {
            ViewBag.MaChatLieu = new SelectList(db.ChatLieus.ToList(),
                "MaChatLieu", "MaChatLieu");
            ViewBag.MaHangSx = new SelectList(db.HangSxes.ToList(),
                "MaHangSx", "MaHangSx");
            ViewBag.MaLoai = new SelectList(db.LoaiSps.ToList(),
                "MaLoai", "Loai");
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
        [Route("ChiTietSanPham")]
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
        [Route("SuaSanPham")]
        [HttpGet]
        public IActionResult SuaSanPham(string maSanPham)
        {
            ViewBag.MaChatLieu = new SelectList(db.ChatLieus.ToList(),
                "MaChatLieu", "MaChatLieu");

            ViewBag.MaHangSx = new SelectList(db.HangSxes.ToList(),
                "MaHangSx", "MaHangSx");

            ViewBag.MaLoai = new SelectList(db.LoaiSps.ToList(),
                "MaLoai", "Loai");
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
                return RedirectToAction("DanhMucSanPham","SanPham");
            }
            return View(sanPham);
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
                return RedirectToAction("DanhMucSanPham", "SanPham");
            }
            var anhSanPham = db.AnhSps.Where(x => x.MaSp == maSanPham);
            if (anhSanPham.Any()) db.RemoveRange(anhSanPham);
            db.Remove(db.DanhMucSps.Find(maSanPham));
            db.SaveChanges();
            TempData["Message"] = "Sản phẩm đã được xóa";
            return RedirectToAction("DanhMucSanPham", "SanPham");
        }
    }
  
}
