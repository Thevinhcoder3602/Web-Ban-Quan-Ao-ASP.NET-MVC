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
    [Route("admin/user")]
    public class UserController : Controller
    {
        QlbanHangContext db = new QlbanHangContext();
        [Route("ListUser")]
        public async Task<IActionResult> ListUser()
        {
            return db.Users != null ?
                        View(await db.Users.ToListAsync()) :
                        Problem("Entity set 'QlbanHangContext.Users'  is null.");
        }

        [Route("ThemUser")]
        [HttpGet]
        public IActionResult ThemUser()
        {
            ViewBag.LoaiUser = new SelectList(db.PhanQuyens.ToList(),
                "LoaiUser", "TenQuyen");
            return View();
        }
        [Route("ThemUser")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemUser(User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("ListUser");
            }
            return View(user);
        }
        [Route("DoiQuyen")]
        [HttpGet]
        public IActionResult DoiQuyen(string name)
        {

            ViewBag.LoaiUser = new SelectList(db.PhanQuyens.ToList(),
                "LoaiUser", "TenQuyen") ;
            var acc = db.Users.Find(name);
            return View(acc);
        }
        [Route("DoiQuyen")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DoiQuyen(User acc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(acc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ListUser", "User");
            }
            return View(acc);
        }
        [Route("XoaTaiKhoan")]
        [HttpGet]
        public IActionResult XoaTaiKhoan(string name)
        {
            TempData["Message"] = "";
            var acc = db.Users.Where(x => x.Username == name).ToList();
            db.Remove(db.Users.Find(name));
            db.SaveChanges();
            TempData["Message"] = "Tài khoản đã được xóa";
            return RedirectToAction("ListUser", "User");
        }

    }
}
