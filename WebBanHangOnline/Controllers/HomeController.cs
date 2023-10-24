using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.Authentication;
using WebBanHangOnline.ViewModels;
using X.PagedList;

namespace WebBanHangOnline.Controllers
{
    public class HomeController : Controller
    {
        QLBanHangContext db = new QLBanHangContext();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        //[Authentication]

        public IActionResult Index(int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page<0 ? 1: page.Value;
            var lstSanPham = db.DanhMucSps.AsNoTracking().OrderBy(x => x.TenSp);
            PagedList<DanhMucSp> lst = new PagedList<DanhMucSp>(lstSanPham, pageNumber, pageSize);
            return View(lst);
        }

        public IActionResult SanPhamTheoLoai(string maloai, int? page = 1)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstSanPham = db.DanhMucSps.AsNoTracking().Where(x => x.MaLoai == maloai).OrderBy(x => x.TenSp);
            PagedList<DanhMucSp> lst = new PagedList<DanhMucSp>(lstSanPham, pageNumber, pageSize);
            ViewBag.maloai = maloai;
            return View(lst);
        }

        public IActionResult ChiTietSanpham(string maSp)
        {
            var sanPham = db.DanhMucSps.SingleOrDefault(x => x.MaSp == maSp);
            var anhSP = db.AnhSps.Where(x=>x.MaSp==maSp).ToList();
            ViewBag.anhSP = anhSP;
            return View(sanPham);
        }

        public IActionResult ProductDetail(string maSp)
        {
            var sanPham = db.DanhMucSps.SingleOrDefault(x => x.MaSp == maSp);
            var anhSP = db.AnhSps.Where(x => x.MaSp == maSp).ToList();
            var homeProductDetailViewModel = new HomeProductDetailViewModel
            {
                danhMucSP = sanPham,
                anhSps = anhSP
            };
            return View(homeProductDetailViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult ThongTinLienHe()
        {
            return View();
        }

        public IActionResult ShopSanPham()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}