using System;
using System.Collections.Generic;

namespace WebBanHangOnline.Models;

public partial class NhanVien
{
    public string MaNhanVien { get; set; } = null!;

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? TenNhanVien { get; set; }

    public DateTime? NgaySinh { get; set; }

    public string? SoDienThoai1 { get; set; }

    public string? SoDienThoai2 { get; set; }

    public string? DiaChi { get; set; }

    public string? ChucVu { get; set; }

    public string? AnhDaiDien { get; set; }

    public string? GhiChu { get; set; }

    public virtual ICollection<HoaDonBan> HoaDonBans { get; set; } = new List<HoaDonBan>();

    public virtual User? UsernameNavigation { get; set; }
}
