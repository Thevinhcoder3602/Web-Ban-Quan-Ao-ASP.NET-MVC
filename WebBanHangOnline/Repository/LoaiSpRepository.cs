using WebBanHangOnline.Models;
namespace WebBanHangOnline.Repository
{
    public class LoaiSpRepository : ILoaiSpRepository
    {
        private readonly QLBanHangContext _context;
        public LoaiSpRepository(QLBanHangContext context)
        {
            _context = context;
        }
        public LoaiSp Add(LoaiSp loaiSp)
        {
            _context.LoaiSps.Add(loaiSp);
            _context.SaveChanges();
            return loaiSp;
        }

        public LoaiSp Delete(string maloaiSp)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LoaiSp> GetAllLoaiSp()
        {
            return _context.LoaiSps;
        }

        public LoaiSp GetLoaiSp(string maloaiSp)
        {
            return _context.LoaiSps.Find(maloaiSp);
        }

        public LoaiSp Update(LoaiSp loaiSp)
        {
            _context.Update(loaiSp);
            _context.SaveChanges();
            return loaiSp;
        }
    }
}
