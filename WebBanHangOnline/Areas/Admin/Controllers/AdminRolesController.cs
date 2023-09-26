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
    public class AdminRolesController : Controller
    {
        private readonly QlbanHangContext _context;

        public AdminRolesController(QlbanHangContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminRole
        public async Task<IActionResult> Index()
        {
              return _context.PhanQuyens != null ? 
                          View(await _context.PhanQuyens.ToListAsync()) :
                          Problem("Entity set 'QlbanHangContext.PhanQuyens'  is null.");
        }

        // GET: Admin/AdminRole/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.PhanQuyens == null)
            {
                return NotFound();
            }

            var phanQuyen = await _context.PhanQuyens
                .FirstOrDefaultAsync(m => m.IdQuyen == id);
            if (phanQuyen == null)
            {
                return NotFound();
            }

            return View(phanQuyen);
        }

        // GET: Admin/AdminRole/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminRole/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdQuyen,TenQuyen,GhiChu")] PhanQuyen phanQuyen)
        {
            if (ModelState.IsValid)
            {
                _context.Add(phanQuyen);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(phanQuyen);
        }

        // GET: Admin/AdminRole/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.PhanQuyens == null)
            {
                return NotFound();
            }

            var phanQuyen = await _context.PhanQuyens.FindAsync(id);
            if (phanQuyen == null)
            {
                return NotFound();
            }
            return View(phanQuyen);
        }

        // POST: Admin/AdminRole/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("IdQuyen,TenQuyen,GhiChu")] PhanQuyen phanQuyen)
        {
            if (id != phanQuyen.IdQuyen)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(phanQuyen);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhanQuyenExists(phanQuyen.IdQuyen))
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
            return View(phanQuyen);
        }

        // GET: Admin/AdminRole/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.PhanQuyens == null)
            {
                return NotFound();
            }

            var phanQuyen = await _context.PhanQuyens
                .FirstOrDefaultAsync(m => m.IdQuyen == id);
            if (phanQuyen == null)
            {
                return NotFound();
            }

            return View(phanQuyen);
        }

        // POST: Admin/AdminRole/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.PhanQuyens == null)
            {
                return Problem("Entity set 'QlbanHangContext.PhanQuyens'  is null.");
            }
            var phanQuyen = await _context.PhanQuyens.FindAsync(id);
            if (phanQuyen != null)
            {
                _context.PhanQuyens.Remove(phanQuyen);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhanQuyenExists(string id)
        {
          return (_context.PhanQuyens?.Any(e => e.IdQuyen == id)).GetValueOrDefault();
        }
    }
}
