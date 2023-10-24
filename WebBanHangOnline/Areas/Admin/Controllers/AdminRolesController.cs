using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebBanHangOnline.Models;

namespace WebBanHangOnline.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/adminroles")]
    public class AdminRolesController : Controller
    {
        QlbanHangContext db = new QlbanHangContext();
        // GET: Admin/AdminRole
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
              return db.PhanQuyens != null ? 
                          View(await db.PhanQuyens.ToListAsync()) :
                          Problem("Entity set 'QlbanHangContext.PhanQuyens'  is null.");
        }

        // GET: Admin/AdminRole/Details/5
        [Route("ChiTietQuyen")]
        public async Task<IActionResult> ChiTietQuyen(string id)
        {
            if (id == null || db.PhanQuyens == null)
            {
                return NotFound();
            }

            var phanQuyen = await db.PhanQuyens
                .FirstOrDefaultAsync(m => m.IdQuyen == id);
            if (phanQuyen == null)
            {
                return NotFound();
            }

            return View(phanQuyen);
        }

        // GET: Admin/AdminRole/Create
        [Route("ThemQuyen")]
        [HttpGet]
        public IActionResult ThemQuyen()
        {
           
            return View();
        }

        // POST: Admin/AdminRole/Create
        [Route("ThemQuyen")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemQuyen(PhanQuyen phanQuyen)
        {
            if (ModelState.IsValid)
            {
                db.PhanQuyens.Add(phanQuyen);
                db.SaveChanges();
                return RedirectToAction("Index", "AdminRoles");
            }
            return View(phanQuyen);
        }

        // GET: Admin/AdminRole/Edit/5
        [Route("SuaQuyen")]
        [HttpGet]
        public async Task<IActionResult> SuaQuyen(string id)
        {
            if (id == null || db.PhanQuyens == null)
            {
                return NotFound();
            }

            var phanQuyen = await db.PhanQuyens.FindAsync(id);
            if (phanQuyen == null)
            {
                return NotFound();
            }
            return View(phanQuyen);
        }

        // POST: Admin/AdminRole/Edit/5
        [Route("SuaQuyen")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaQuyen(PhanQuyen phanQuyen)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phanQuyen).State = EntityState.Modified; //Update sp
                db.SaveChanges();
                return RedirectToAction("Index", "AdminRole");
            }
            return View(phanQuyen);
        }

        // GET: Admin/AdminRole/Delete/5
        [Route("XoaQuyen")]
        [HttpGet]
        public async Task<IActionResult> XoaQuyen(string id)
        {
            if (id == null || db.PhanQuyens == null)
            {
                return NotFound();
            }

            var phanQuyen = await db.PhanQuyens
                .FirstOrDefaultAsync(m => m.IdQuyen == id);
            if (phanQuyen == null)
            {
                return NotFound();
            }

            return View(phanQuyen);
        }

        // POST: Admin/AdminRole/Delete/5
        [Route("XoaQuyen")]
        [HttpPost, ActionName("XoaQuyen")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> XoaQuyenConfirmed(string id)
        {
            try
            {
                if (db.PhanQuyens == null)
                {
                    return Problem("Entity set 'QlbanHangContext.PhanQuyens' is null.");
                }

                var phanQuyen = await db.PhanQuyens.FindAsync(id);
                if (phanQuyen == null)
                {
                    return NotFound(); // Quyền không tồn tại
                }

                db.PhanQuyens.Remove(phanQuyen);
                await db.SaveChangesAsync();

                // Chuyển hướng quay lại trang Index của AdminRoles sau khi xóa thành công
                return RedirectToAction("Index", "AdminRoles");
            }
            catch (Exception ex)
            {
                // Ghi log lỗi hoặc xử lý lỗi theo cách của bạn
                return Problem("An error occurred while deleting the role. Error: " + ex.Message);
            }
        }


        private bool PhanQuyenExists(string id)
        {
          return (db.PhanQuyens?.Any(e => e.IdQuyen == id)).GetValueOrDefault();
        }
    }
}
