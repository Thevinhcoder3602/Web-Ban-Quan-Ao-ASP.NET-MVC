using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebBanHangOnline.Extention;
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
                // Băm mật khẩu trước khi lưu vào cơ sở dữ liệu
               
                string hashPassword = user.Password.ToMD5();
                // Gán mật khẩu đã băm vào đối tượng User
                user.Password = hashPassword;
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

            // Tìm người dùng theo tên
            var userToDelete = db.Users.FirstOrDefault(x => x.Username == name);

            if (userToDelete != null)
            {
                // Tìm khách hàng tương ứng
                var khachHangToDelete = db.KhachHangs.FirstOrDefault(kh => kh.Username == name);

                if (khachHangToDelete != null)
                {
                    // Xóa khách hàng
                    db.KhachHangs.Remove(khachHangToDelete);
                }

                // Tìm nhân viên tương ứng
                var nhanVienToDelete = db.NhanViens.FirstOrDefault(nv => nv.Username == name);

                if (nhanVienToDelete != null)
                {
                    // Xóa nhân viên
                    db.NhanViens.Remove(nhanVienToDelete);
                }

                // Xóa người dùng
                db.Users.Remove(userToDelete);
                db.SaveChanges();

                TempData["Message"] = "Tài khoản và thông tin liên quan đã được xóa";
            }
            else
            {
                TempData["Message"] = "Không tìm thấy tài khoản";
            }

            return RedirectToAction("ListUser", "User");
        }


    }
}
