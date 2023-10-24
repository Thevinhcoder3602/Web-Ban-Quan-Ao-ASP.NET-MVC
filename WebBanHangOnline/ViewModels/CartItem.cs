using WebBanHangOnline.Models;

namespace WebBanHangOnline.ViewModels

{
    public class CartItem
    {

        public  DanhMucSp sanpham { get; set; }
        public int soLuong { get; set; }
        public double tongTien => (double)(soLuong * sanpham.GiaLonNhat);
    }
}
 