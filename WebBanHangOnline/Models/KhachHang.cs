using System;
using System.Collections.Generic;

namespace WebBanHangOnline.Models
{
    public partial class KhachHang
    {
        public KhachHang()
        {
            HoaDonBans = new HashSet<HoaDonBan>();
        }

        public string MaKhanhHang { get; set; } = null!;
        public string? Username { get; set; }
        public string? TenKhachHang { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string? SoDienThoai { get; set; }
        public string? DiaChi { get; set; }
        public byte? LoaiKhachHang { get; set; }
        public string? AnhDaiDien { get; set; }
        public string? GhiChu { get; set; }

        public virtual User? UsernameNavigation { get; set; }
        public virtual ICollection<HoaDonBan> HoaDonBans { get; set; }
    }
}
