using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebBanHangOnline.Models;
using X.PagedList;


namespace WebBanHangOnline.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin")]
    [Route("admin/nhanviens")]
    public class NhanVienController : Controller
    {
        QLBanHangContext db = new QLBanHangContext();
        
        public IActionResult Index()
        {
            return View();
        }
        [Route("ListNhanVien")]
        public IActionResult ListNhanVien(int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lst = db.NhanViens.AsNoTracking().
                OrderBy(x => x.MaNhanVien);
            PagedList<NhanVien> list = new(lst,
                pageNumber, pageSize);
            return View(list);
        }
        [Route("ThemNhanVien")]
        [HttpGet]
        public IActionResult ThemNhanVien()
        {
            ViewBag.Username = new SelectList(db.Users.ToList(),
                "Username", "Username");
            return View();
        }
        [Route("ThemNhanVien")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemNhanVien(NhanVien nhanvien)
        {
            if (ModelState.IsValid)
            {
                db.NhanViens.Add(nhanvien);
                db.SaveChanges();
                return RedirectToAction("ListNhanVien");
            }
            return View(nhanvien);
        }
        [Route("ChiTietNhanVien")]
        public async Task<IActionResult> ChiTietNhanVien(string maNV)
        {
            if (maNV == null || db.NhanViens == null)
            {
                return NotFound();
            }

            var Emlp = await db.NhanViens
                .FirstOrDefaultAsync(m => m.MaNhanVien == maNV);
            if (Emlp == null)
            {
                return NotFound();
            }

            return View(Emlp);
        }
        [Route("SuaNhanVien")]
        [HttpGet]
        public IActionResult SuaNhanVien(string maNV)
        {
            ViewBag.Username = new SelectList(db.Users.ToList(),
                "Username", "Username");
            var NV = db.NhanViens.Find(maNV);
            return View(NV);
        }
        [Route("SuaNhanVien")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaNhanVien(NhanVien nhanVien)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nhanVien).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ListNhanVien", "NhanVien");
            }
            return View(nhanVien);
        }

        [Route("XoaNhanVien")]
        [HttpGet]
        public IActionResult XoaNhanVien(string manv)
        {
            TempData["Message"] = "";
            var acc = db.NhanViens.Where(x => x.MaNhanVien == manv).ToList();
            db.Remove(db.NhanViens.Find(manv));
            db.SaveChanges();
            TempData["Message"] = "Nhân viên đã được xóa";
            return RedirectToAction("ListNhanVien", "NhanVien");
        }

    }

}
