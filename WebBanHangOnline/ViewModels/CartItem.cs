using WebBanHangOnline.Models;
using WebBanHangOnline.Models.ProductModels;

namespace WebBanHangOnline.ViewModels

{
    public class CartItem
    {

        public  DanhMucSp product { get; set; }
        public int amount { get; set; }
        public double TotalMoney => (double)(amount * product.GiaLonNhat.Value);
    }
}
 