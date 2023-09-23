using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.ProductModels;

namespace WebBanHangOnline.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductAPIController : ControllerBase
    {
        QlbanHangContext db = new QlbanHangContext();
        [HttpGet]
        public IEnumerable<Product> GetAllProducts()
        {
            var sanPham = (from p in db.DanhMucSps
                           select new Product
                           {
                               MaSp = p.MaSp,
                               TenSp = p.TenSp,
                               MaLoai = p.MaLoai,
                               AnhDaiDien = p.AnhDaiDien,
                               GiaLonNhat = p.GiaNhoNhat,
                           }).ToList();
            return sanPham;
        }



        [HttpGet("{maloai}")]
        public IEnumerable<Product> GetProductsByCategory(string maloai)
        {
            var sanPham = (from p in db.DanhMucSps
                           where p.MaLoai == maloai
                           select new Product
                           {
                               MaSp = p.MaSp,
                               TenSp = p.TenSp,
                               MaLoai = p.MaLoai,
                               AnhDaiDien = p.AnhDaiDien,
                               GiaLonNhat = p.GiaLonNhat,
                           }).ToList();
            return sanPham;
        }
    }
}
