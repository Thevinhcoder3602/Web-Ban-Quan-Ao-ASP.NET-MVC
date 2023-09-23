using System;
using System.Collections.Generic;

namespace WebBanHangOnline.Models;

public partial class HoaDonBan
{
    public string MaHoaDon { get; set; } = null!;

    public DateTime? NgayHoaDon { get; set; }

    public string? MaKhachHang { get; set; }

    public string? MaNhanVien { get; set; }

    public decimal? TongTienHd { get; set; }

    public double? GiamGiaHd { get; set; }

    public byte? PhuongThucThanhToan { get; set; }

    public string? GhiChu { get; set; }

    public virtual ICollection<ChiTietHdban> ChiTietHdbans { get; set; } = new List<ChiTietHdban>();

    public virtual KhachHang? MaKhachHangNavigation { get; set; }

    public virtual NhanVien? MaNhanVienNavigation { get; set; }
}
