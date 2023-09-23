using WebBanHangOnline.Models;
namespace WebBanHangOnline.Repository
{
    public interface ILoaiSpRepository
    {
        LoaiSp Add(LoaiSp loaiSp);

        LoaiSp Update(LoaiSp loaiSp);

        LoaiSp Delete(String maloaiSp);

        LoaiSp GetLoaiSp(String maloaiSp);

        IEnumerable<LoaiSp> GetAllLoaiSp();
    }
}
