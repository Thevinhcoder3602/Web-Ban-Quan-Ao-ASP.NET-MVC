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
    [Area("Admin")]
    public class HoaDonBansController : Controller
    {
        private readonly QLBanHangContext _context;

        public HoaDonBansController(QLBanHangContext context)
        {
            _context = context;
        }

        // GET: Admin/HoaDonBans
        public async Task<IActionResult> Index()
        {
            var qLBanHangContext = _context.HoaDonBans.Include(h => h.MaKhachHangNavigation).Include(h => h.MaNhanVienNavigation);
            return View(await qLBanHangContext.ToListAsync());
        }

        // GET: Admin/HoaDonBans/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.HoaDonBans == null)
            {
                return NotFound();
            }

            var hoaDonBan = await _context.HoaDonBans
                .Include(h => h.MaKhachHangNavigation)
                .Include(h => h.MaNhanVienNavigation)
                .FirstOrDefaultAsync(m => m.MaHoaDon == id);
            if (hoaDonBan == null)
            {
                return NotFound();
            }

            return View(hoaDonBan);
        }

        // GET: Admin/HoaDonBans/Create
        public IActionResult Create()
        {
            ViewData["MaKhachHang"] = new SelectList(_context.KhachHangs, "MaKhanhHang", "MaKhanhHang");
            ViewData["MaNhanVien"] = new SelectList(_context.NhanViens, "MaNhanVien", "MaNhanVien");
            return View();
        }

        // POST: Admin/HoaDonBans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaHoaDon,NgayHoaDon,MaKhachHang,MaNhanVien,TongTienHd,GiamGiaHd,PhuongThucThanhToan,GhiChu")] HoaDonBan hoaDonBan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hoaDonBan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaKhachHang"] = new SelectList(_context.KhachHangs, "MaKhanhHang", "MaKhanhHang", hoaDonBan.MaKhachHang);
            ViewData["MaNhanVien"] = new SelectList(_context.NhanViens, "MaNhanVien", "MaNhanVien", hoaDonBan.MaNhanVien);
            return View(hoaDonBan);
        }

        // GET: Admin/HoaDonBans/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.HoaDonBans == null)
            {
                return NotFound();
            }

            var hoaDonBan = await _context.HoaDonBans.FindAsync(id);
            if (hoaDonBan == null)
            {
                return NotFound();
            }
            ViewData["MaKhachHang"] = new SelectList(_context.KhachHangs, "MaKhanhHang", "MaKhanhHang", hoaDonBan.MaKhachHang);
            ViewData["MaNhanVien"] = new SelectList(_context.NhanViens, "MaNhanVien", "MaNhanVien", hoaDonBan.MaNhanVien);
            return View(hoaDonBan);
        }

        // POST: Admin/HoaDonBans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaHoaDon,NgayHoaDon,MaKhachHang,MaNhanVien,TongTienHd,GiamGiaHd,PhuongThucThanhToan,GhiChu")] HoaDonBan hoaDonBan)
        {
            if (id != hoaDonBan.MaHoaDon)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hoaDonBan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HoaDonBanExists(hoaDonBan.MaHoaDon))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaKhachHang"] = new SelectList(_context.KhachHangs, "MaKhanhHang", "MaKhanhHang", hoaDonBan.MaKhachHang);
            ViewData["MaNhanVien"] = new SelectList(_context.NhanViens, "MaNhanVien", "MaNhanVien", hoaDonBan.MaNhanVien);
            return View(hoaDonBan);
        }

        // GET: Admin/HoaDonBans/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.HoaDonBans == null)
            {
                return NotFound();
            }

            var hoaDonBan = await _context.HoaDonBans
                .Include(h => h.MaKhachHangNavigation)
                .Include(h => h.MaNhanVienNavigation)
                .FirstOrDefaultAsync(m => m.MaHoaDon == id);
            if (hoaDonBan == null)
            {
                return NotFound();
            }

            return View(hoaDonBan);
        }

        // POST: Admin/HoaDonBans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.HoaDonBans == null)
            {
                return Problem("Entity set 'QLBanHangContext.HoaDonBans'  is null.");
            }
            var hoaDonBan = await _context.HoaDonBans.FindAsync(id);
            if (hoaDonBan != null)
            {
                _context.HoaDonBans.Remove(hoaDonBan);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HoaDonBanExists(string id)
        {
          return (_context.HoaDonBans?.Any(e => e.MaHoaDon == id)).GetValueOrDefault();
        }
    }
}
