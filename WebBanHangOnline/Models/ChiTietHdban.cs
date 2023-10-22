using System;
using System.Collections.Generic;

namespace WebBanHangOnline.Models;

public partial class ChiTietHdban
{
    public string MaHoaDon { get; set; } = null!;

    public string MaChiTietSp { get; set; } = null!;

    public int? SoLuongBan { get; set; }

    public decimal? DonGiaBan { get; set; }

    public double? GiamGia { get; set; }

    public string? GhiChu { get; set; }

    public virtual ChiTietSp MaChiTietSpNavigation { get; set; } = null!;

    public virtual HoaDonBan MaHoaDonNavigation { get; set; } = null!;
}
